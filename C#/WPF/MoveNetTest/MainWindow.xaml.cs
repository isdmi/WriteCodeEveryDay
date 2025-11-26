using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.Toolkit.Uwp.Notifications;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;


namespace MoveNetTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly DispatcherTimer _timer;
        private readonly VideoCapture _camera;
        private readonly InferenceSession _session;

        public MainWindow()
        {
            InitializeComponent();

            // カメラ
            _camera = new VideoCapture(0);
            if (!_camera.IsOpened())
            {
                MessageBox.Show("カメラを開けません");
                return;
            }

            // AIモデル読み込み
            _session = new InferenceSession("movenet_single_pose_lightning.onnx");

            // 5分ごとに実行
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(5);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            StatusText.Text = "5分ごとに姿勢をチェックします";
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            StatusText.Text = "姿勢を撮影しています...";

            // カメラから取得
            Mat frame = new Mat();
            _camera.Read(frame);
            if (frame.Empty())
                return;

            // AI推論して keypoints を取得
            var keypoints = RunPoseEstimation(frame);

            // 姿勢判定
            bool badPosture = IsBadPosture(keypoints);

            if (badPosture)
            {
                ShowNotification("姿勢が崩れています", "背筋を伸ばしましょう！");
                StatusText.Text = "猫背が検出されました";
            }
            else
            {
                StatusText.Text = "姿勢は良好です";
            }
        }

        /// <summary>
        /// MoveNet 推論
        /// </summary>
        private float[] RunPoseEstimation(Mat frame)
        {
            var resized = frame.Resize(new OpenCvSharp.Size(192, 192));
            resized.ConvertTo(resized, MatType.CV_32FC3, 1.0 / 255);

            var imgData = new float[1 * 192 * 192 * 3];
            int idx = 0;

            // NHWC 形式へ変換
            for (int y = 0; y < 192; y++)
            {
                for (int x = 0; x < 192; x++)
                {
                    Vec3f pixel = resized.At<Vec3f>(y, x);
                    imgData[idx++] = pixel[2]; // R
                    imgData[idx++] = pixel[1]; // G
                    imgData[idx++] = pixel[0]; // B
                }
            }

            var tensor = new DenseTensor<float>(imgData, new[] { 1, 192, 192, 3 });

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("image", tensor)
            };

            IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _session.Run(inputs);
            return results.First().AsEnumerable<float>().ToArray();
        }

        /// <summary>
        /// 猫背判定ロジック（耳-肩の角度を見る）
        /// </summary>
        private bool IsBadPosture(float[] keypoints)
        {
            // 17個 * (x,y,score) = 51要素
            (float x, float y) RightEar = (keypoints[0 * 3], keypoints[0 * 3 + 1]);
            (float x, float y) RightShoulder = (keypoints[6 * 3], keypoints[6 * 3 + 1]);

            // 角度計算
            double dx = RightEar.x - RightShoulder.x;
            double dy = RightEar.y - RightShoulder.y;
            double angle = Math.Atan2(dy, dx) * 180.0 / Math.PI;

            // 頭が前に20°以上出ていたら猫背
            return Math.Abs(angle) > 20;
        }

        /// <summary>
        /// Windows通知
        /// </summary>
        private void ShowNotification(string title, string message)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .Show();
        }
    }
}