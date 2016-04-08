using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUtility {
    public class PathBorder : Viewbox {
        protected override Size MeasureOverride(Size constraint) {
            return new Size(constraint.Width + 5, constraint.Height + 5);
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.Black, new Pen(), new Rect(0, 0, ActualWidth, ActualHeight));
            //drawingContext.DrawEllipse(Brushes.Black, new Pen(), new Point((int)ActualWidth / 2, (int)ActualHeight / 2), ActualWidth / 2, ActualHeight / 2);
        }
    }
}
