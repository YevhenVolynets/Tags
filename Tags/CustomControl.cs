using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Tags
{
    public class CustomControl : SKCanvasView
    {
        public CustomControl()
        {
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var size = CanvasSize;
            var leftRightMargin = size.Width / 9.2f;
            var deltaDistance = leftRightMargin * 0.1f;
            var startCornerX = leftRightMargin - (leftRightMargin / 9) * 2;
            var endCornerX = size.Width - startCornerX;
            var cornerY = size.Height / 5;
            var cornerCenterY = size.Height / 20;
            var startCornerCenterX = leftRightMargin - (leftRightMargin / 9);
            var endCornerCenterX = size.Width - startCornerCenterX;
            canvas.Clear();

            SKPaint strokePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                StrokeWidth = 4,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true
            };

            using (SKPath path = new SKPath())
            {
                path.MoveTo(new SKPoint(0, size.Height));


                path.LineTo(new SKPoint(startCornerX, cornerY));

                path.ArcTo(new SKPoint(startCornerCenterX, cornerCenterY), new SKPoint(leftRightMargin + deltaDistance, 0), 100);

                
                path.LineTo(new SKPoint(size.Width - leftRightMargin - deltaDistance, 0));

                path.ArcTo(new SKPoint(endCornerCenterX, cornerCenterY), new SKPoint(endCornerX, cornerY), 100);

                path.LineTo(new SKPoint(size.Width, size.Height));

                path.Close();


                canvas.DrawPath(path, strokePaint);
            }
        }
    }
}
