using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RectanglesOnImage_WPF_App
{
    /// <summary>
    /// Defines data-model for displayable rectangle
    /// </summary>
	class RectangleDataModel : INotifyPropertyChanged
	{
        #region Public Properties

        // <summary>
        /// X cordinate of rectangle. X-position of the top-left corner
        /// </summary>
        public double X
        {
            get
            {
				return mX;
            }
            set
            {
				mX = value;

				OnPropertyChanged( "X" );
            }
        }

        /// <summary>
        /// Y cordinate of rectangle. Y-position of the top-left corner
        /// </summary>
        public double Y
        {
            get
            {
				return mY;
            }
            set
            {
				mY = value;

				OnPropertyChanged( "Y" );
            }
        }

        /// <summary>
        /// Width of rectangle.
        /// </summary>
        public double Width
        {
            get
            {
				return mWidth;
            }
            set
            {
				mWidth = value;

				OnPropertyChanged( "Width" );
            }
        }

        /// <summary>
        /// Height of rectangle.
        /// </summary>
        public double Height
        {
            get
            {
				return mHeight;
            }
            set
            {
				mHeight = value;

				OnPropertyChanged( "Height" );
            }
        }

        /// <summary>
        /// Color of rectangle.
        /// </summary>
        public Color Color
        {
            get
            {
				return mColor;
            }
            set
            {
				mColor = value;

				OnPropertyChanged( "Color" );
            }
        }

        /// <summary>
        /// true if the rectangle is currently selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
				return mIsSelected;
            }
            set
            {
				mIsSelected = value;

				OnPropertyChanged( "IsSelected" );
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default Constructor. 
        /// Sets all the position and lenght to 0.0, color to black, and selected to false
        /// </summary>
        public RectangleDataModel()
		{
			this.mX = 0.0;
			this.mY = 0.0;
			this.mWidth = 0.0;
			this.mHeight = 0.0;
			this.mColor = Colors.Black;
			this.mIsSelected = false;
		}

        /// <summary>
        /// Consturctor.
        /// </summary>
        /// <param name="aX">double. X-position of the top-left corner of the rectangle</param>
        /// <param name="aY">double. Y-position of the top-left corner of the rectangle</param>
        /// <param name="aWidth">double. Width of the rectangle</param>
        /// <param name="aHeight">double. Height of the rectangle</param>
        /// <param name="aColor">Color. Color of the rectangle</param>
        /// <param name="aIsSelected">bool. true if the rectangle is selected</param>
        public RectangleDataModel( double aX , double aY , double aWidth , double aHeight , Color aColor, bool aIsSelected )
		{
            this.mX = aX;
            this.mY = aY;
            this.mWidth = aWidth;
            this.mHeight = aHeight;
            this.mColor = aColor;
            this.mIsSelected = aIsSelected;
        }

		#endregion

		# region Public Methods
		/// <summary>
		/// converts the rectangle data model to rectangle and returns it
		/// </summary>
		/// <returns></returns>
		public Rectangle convertToRectangle()
        {
            Rectangle rect = new Rectangle();
            rect.Width = mWidth;
            rect.Height = mHeight;
            rect.Fill = new SolidColorBrush( mColor );
            Canvas.SetTop( rect , Y );
            Canvas.SetLeft( rect , X );
            return rect;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The property that has a new value.</param>
		protected void OnPropertyChanged( [CallerMemberName] string propertyName = null )
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if( handler != null )
			{
				var e = new PropertyChangedEventArgs( propertyName );
				handler( this , e );
			}
		}

		#endregion

		#region Private Data Members

		/// <summary>
		/// X cordinate of rectangle. X-position of the top-left corner
		/// </summary>
		private double mX;

		/// <summary>
		/// Y cordinate of rectangle. Y-position of the top-left corner
		/// </summary>
		private double mY;

		/// <summary>
		/// Height of rectangle.
		/// </summary>
		private double mWidth;

		/// <summary>
		/// Length of rectangle
		/// </summary>
		private double mHeight;

		/// <summary>
		/// Color of rectangle
		/// </summary>
		private Color mColor;

		// <summary>
		/// true if the rectangle is currently selected
		/// </summary>
		private bool mIsSelected;

        #endregion
    }
}
