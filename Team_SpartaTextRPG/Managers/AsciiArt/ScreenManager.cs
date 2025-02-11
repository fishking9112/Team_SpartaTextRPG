

using System.Drawing;
using System.Text;
using Emgu.CV;
using Mat = Emgu.CV.Mat;
using VidioCapture = Emgu.CV.VideoCapture;

namespace Team_SpartaTextRPG
{
    class ScreenManager : Helper.Singleton<ScreenManager>
    {
        // 취소를 위해 필요
        public List<CancellationTokenSource> _ctsList = new List<CancellationTokenSource>();
        // 동영상 그림 그릴때 다음씬으로 갈 때 안곂이체 하기 위해서 필요
        public int bigFrame = 0;

        //! ClearScreen() 하고 사용 할 것
        // 전체 스크린 영상 (영상들은 곂치게 생성하지 말 것)
        public void AsyncVideo(string _vidio, ConsoleColor _color = ConsoleColor.Gray, bool _isContinue = true, bool _isReversal = false, int _frame = 33)
        {
            AddScreen(out CancellationTokenSource temp);

            Task.Run(() => PlayVideo(_vidio, temp.Token, 0, 0, PanelManager.instance.gamePanelX, PanelManager.instance.screenPanelY, _color, _isContinue, _isReversal, _frame), temp.Token); // 비동기 입력 감지 시작
        }

        //! ClearScreen() 하고 사용 할 것
        // 유닛 하나 영상 (영상들은 곂치게 생성하지 말 것)
        public void AsyncUnitVideo(string _vidio, int startX, int startY, int videoSizeX, int videoSizeY, ConsoleColor _color = ConsoleColor.Gray, bool _isContinue = true, bool _isReversal = false, int _frame = 33)
        {
            AddScreen(out CancellationTokenSource temp);

            Task.Run(() => PlayVideo(_vidio, temp.Token, startX, startY, videoSizeX, videoSizeY, _color, _isContinue, _isReversal, _frame), temp.Token); // 비동기 입력 감지 시작
        }

        //! ClearAction() 하고 사용 할 것
        public void AsyncImage(string _image, int _startX = 0, int _startY = 0, int imageSizeX = 0, int imageSizeY = 0, ConsoleColor _color = ConsoleColor.Gray)
        {
            imageSizeX = imageSizeX == 0 ? PanelManager.instance.gamePanelX : imageSizeX; // 아무값도 없다면 전체크기
            imageSizeY = imageSizeY == 0 ? PanelManager.instance.screenPanelY : imageSizeY; // 아무값도 없다면 전체크기
            ShowImage(_image, _startX, _startY, imageSizeX, imageSizeY, _color);
        }

        //! ClearAction() 하고 사용 할 것
        public void AsyncText(StringBuilder _text, int _startX = 1, int _startY = 1, ConsoleColor _color = ConsoleColor.Gray)
        {
            ShowText(_text, _startX, _startY, _color);
        }
        //! ClearAction() 하고 사용 할 것
        public void AsyncText(string _text, int _startX = 1, int _startY = 1, ConsoleColor _color = ConsoleColor.Gray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_text);
            ShowText(sb, _startX, _startY, _color);
        }

        // Screen Video들 작동 브레이크 설치!
        public void AddScreen(out CancellationTokenSource temp)
        {
            temp = new CancellationTokenSource();
            _ctsList.Add(temp);
        }

        // Screen Video들 브레이크 밟아서 작동 멈추기!
        public void ClearScreen()
        {
            // 모든 action 삭제
            if (_ctsList.Count != 0)
            {
                foreach (var cts in _ctsList)
                {
                    cts.Cancel();  // 기존 작업을 취소하도록 요청
                    cts.Dispose(); // 이전 CancellationTokenSource 객체 해제
                }
                _ctsList.Clear();
            }

            Utill.Sleep(bigFrame); // 동영상의 최대 프레임만큼 기다려서 아스키 안곂치게 하기
            bigFrame = 0;
            PanelManager.instance.DrawScreenPanel();
        }

        // Video 실행
        private async Task PlayVideo(string _videoName, CancellationToken _token, int startX, int startY, int panelX, int panelY, ConsoleColor _color = ConsoleColor.Gray, bool _isContinue = true, bool _isReversal = false, int _frame = 33)
        {
            string asciiChars = _isReversal ? "@XWwli:,.  " : "  .,:ilwWX@";
            var capture = new VidioCapture(_videoName);
            var img = new Mat();
            StringBuilder sb = new();
            try
            {
                while (capture.IsOpened)
                {
                    // 취소 요청이 있는지 체크
                    _token.ThrowIfCancellationRequested();
                    capture.Read(img);

                    if (img.Cols == 0)
                    {
                        if (_isContinue) // 영상이 끝나도 계속해서 반복하게 함
                        {
                            capture = new VidioCapture(_videoName);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }

                    // 비트맵 변환 및 크기 조정
                    var bit = img.ToBitmap();
                    var resized = new System.Drawing.Size(panelX * 2, panelY);
                    Bitmap bitResized = new(bit, resized);

                    // 버퍼에 ASCII 변환 결과 저장
                    for (int i = 0; i < bitResized.Height; i++)
                    {
                        for (int j = 1; j < bitResized.Width; j++)
                        {
                            var pixel = bitResized.GetPixel(j, i);
                            var avg = (int)(pixel.R * 0.3f + pixel.G * 0.59f + pixel.B * 0.11f);
                            sb.Append(asciiChars[avg * 10 / 255 % asciiChars.Length]);
                        }
                        sb.AppendLine();
                    }

                    // 버퍼에 저장된 ASCII 변환된대로 출력
                    CursorManager.instance.CurserPointUse(() =>
                    {
                        if(_token.CanBeCanceled){
                            // 만약 취소가 안됬다면 그리기
                            Console.ForegroundColor = _color; //그림 색상 변경
                            if (!GameManager.instance.isPlaying) return;
                            var sbSplit = sb.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                            for (int i = 0; i < sbSplit.Length; i++)
                            {
                                Console.SetCursorPosition(startX + 2, i + 3 + startY);
                                Console.Write(sbSplit[i].ToString());
                            }
                            sb.Clear();
                            Console.ResetColor();
                        }

                    });
                    if (bigFrame < _frame) bigFrame = _frame;
                    await Task.Delay(_frame); // 프레임 조절
                }
            }
            catch (OperationCanceledException)
            {
                // 없애야 하는데...
                // PanelManager.instance.DrawScreenPanel();
            }
        }

        // 이미지 뷰
        private void ShowImage(string _imageName, int startX, int startY, int panelX, int panelY, ConsoleColor _color = ConsoleColor.Gray, bool _isReversal = false)
        {
            string asciiChars = _isReversal ? "@XWwli:,.  " : "  .,:ilwWX@";
            Bitmap img = new(_imageName);
            img = new(img, new Size(panelX * 2, panelY));


            StringBuilder sb = new();

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 1; j < img.Width; j++)
                {
                    var pixel = img.GetPixel(j, i);
                    var avg = (int)(pixel.R * 0.3f + pixel.G * 0.59f + pixel.B * 0.11f);

                    sb.Append(asciiChars[avg * 10 / 255 % asciiChars.Length]);
                }
                sb.AppendLine();
            }

            CursorManager.instance.CurserPointUse(() =>
            {
                Console.ForegroundColor = _color; //그림 색상 변경
                if (!GameManager.instance.isPlaying) return;
                var sbSplit = sb.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < sbSplit.Length; i++)
                {
                    Console.SetCursorPosition(startX + 2, i + 3 + startY);
                    Console.Write(sbSplit[i].ToString());
                }
                sb.Clear();
                Console.ResetColor();
            });
        }

        // 텍스트 뷰
        private void ShowText(StringBuilder _text, int startX, int startY, ConsoleColor _color = ConsoleColor.Gray)
        {
            CursorManager.instance.CurserPointUse(() =>
            {
                Console.ForegroundColor = _color; //그림 색상 변경
                if (!GameManager.instance.isPlaying) return;
                var sbSplit = _text.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < sbSplit.Length; i++)
                {
                    Console.SetCursorPosition(startX + 2, i + 3 + startY);
                    Console.Write(sbSplit[i].ToString());
                }
                _text.Clear();
                Console.ResetColor();
            });
        }

    }
}