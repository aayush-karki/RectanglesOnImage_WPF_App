using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RectanglesOnImage_WPF_App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		#region Public Properties
		
		/// <summary>
		/// Current active color
		/// </summary>
		public Color CurrActiveColor
		{
			set
			{
				mCurrActiveColor = value;

				OnPropertyChanged( "CurrActiveColor" );
			}
			get
			{
				return mCurrActiveColor;
			}
		}


		#endregion

		public MainWindow()
		{
			InitializeComponent();

			// Initializing member variables
			initializeMemberVariables();
		}

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

		#region Private Properties

		/// <summary>
		/// Current Active Tool
		/// </summary>
		private ToolEnum CurrentActiveTool
		{
			set
			{
				if( mCurrActiveTool == value )
				{
					return;
				}

				// deactive curr tool, save new tool, activate new tool
				deactivateToolBtn( getCurrActiveToolButton() );
				mCurrActiveTool = value;
				activateToolBtn( getCurrActiveToolButton() );

				// colaps the canvas when not need
				// this is done so that it does not interfare with othertools
				canvas_drawing.Visibility = ( mCurrActiveTool == ToolEnum.Rectangle ) ? Visibility.Visible : Visibility.Collapsed;
			}
			get
			{
				return mCurrActiveTool;
			}
		}

		#endregion

		#region Private Events

		/// <summary>
		/// Event raised when load image button is clicked. Allows user to load a image file to canvas.
		/// </summary>
		private void btn_loadImg_Click( object sender , RoutedEventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image|*.jpg;*.jpeg;*png";

			if( openFileDialog.ShowDialog() == true )
			{
				// loading the selecting image
				mBgImage = new BitmapImage( new Uri( ( string ) openFileDialog.FileName ) );
				img_bgImage.Source = mBgImage;

				// drawing a border around the image
				rec_imgBorder.Width = mBgImage.Width + mBorderThickness;
				rec_imgBorder.Height = mBgImage.Height + mBorderThickness;

				// image has been loaded
				mImageLoaded = true;
			}
		}

		/// <summary>
		/// Event raised when save image button is clicked. Allows user to save the canvas as image file.
		/// </summary>
		private void btn_SaveImg_Click( object sender , RoutedEventArgs e )
		{

		}

		/// <summary>
		/// Event raised to set the current active color to red.
		/// </summary>
		private void btn_colorPicker__red_Click( object sender , RoutedEventArgs e )
		{
			CurrActiveColor = Colors.Red;
		}

		/// <summary>
		/// Event raised to set the current active color to blue.
		/// </summary>
		private void btn_colorPicker__blue_Click( object sender , RoutedEventArgs e )
		{
			CurrActiveColor = Colors.Blue;
		}

		/// <summary>
		/// Event raised to set the current active color to green.
		/// </summary>
		private void btn_colorPicker__green_Click( object sender , RoutedEventArgs e )
		{
			CurrActiveColor = Colors.Green;
		}


		/// <summary>
		/// Event raised to set the current active color to black.
		/// </summary>
		private void btn_colorPicker__black_Click( object sender , RoutedEventArgs e )
		{
			CurrActiveColor = Colors.Black;
		}

		/// <summary>
		/// Event raised to set the current active color to white.
		/// </summary>
		private void btn_colorPicker__white_Click( object sender , RoutedEventArgs e )
		{
			CurrActiveColor = Colors.White;
		}


		/// <summary>
		/// Event raised when fill tool button is clicked. Sets fill tool as active tool.
		/// </summary>
		private void btn_fillTool_Click( object sender , RoutedEventArgs e )
		{
			CurrentActiveTool = ToolEnum.Fill;
		}

		/// <summary>
		/// Event raised when rectangle tool button is clicked. Sets rectangle tool as active tool.
		/// </summary>
		private void btn_rectangleTool_Click( object sender , RoutedEventArgs e )
		{
			// only allow to select rectangle tool if image is loaded 
			if(mImageLoaded)
			{
				CurrentActiveTool = ToolEnum.Rectangle;
			}
		}

		/// <summary>
		/// Event raised when hand tool button is clicked. Sets hand tool as active tool.
		/// </summary>
		private void btn_handTool_Click( object sender , RoutedEventArgs e )
		{
			CurrentActiveTool = ToolEnum.Hand;
		}

		/// <summary>
		/// Event raised when mouse is pressed down when the pointer is over a rectangle.
		/// </summary>
		private void Rectangle_MouseDown( object sender , MouseButtonEventArgs e )
		{
			// if active tool is not hand and not fill then do nothing 
			if( CurrentActiveTool != ToolEnum.Hand && CurrentActiveTool != ToolEnum.Fill )
			{
				return;
			}

			Rectangle rectangle = ( Rectangle ) sender;
			RectangleDataModel selRectangle = ( RectangleDataModel ) rectangle.DataContext;
			
			// setting the selected rectangle to true
			selRectangle.IsSelected = true;
		
			// for fill tool
			if( CurrentActiveTool == ToolEnum.Fill )
			{
				// storing the rectangle to compare to when mouse is up
				storedSelectedMouseDownRectangle = rectangle;
				return;
			}

			// for hand tool

			// setting the draging to true
			mDragingRectangles = true;
			mMouseStartPoint = e.GetPosition( content );
			rectangle.CaptureMouse();

			e.Handled = true;
		}

		/// <summary>
		/// Event raised when mouse is moved when the pointer is over a rectangle and the mouse is pressed down.
		/// </summary>
		private void Rectangle_MouseMove( object sender , MouseEventArgs e )
		{

		}

		/// <summary>
		/// Event raised when mouse is released when the pointer is over a rectangle and mouse button is pressed down.
		/// </summary>
		private void Rectangle_MouseUp( object sender , MouseButtonEventArgs e )
		{
			// if active tool is not hand and not fill then do nothing 
			if( CurrentActiveTool != ToolEnum.Hand && CurrentActiveTool != ToolEnum.Fill )
			{
				return;
			}

			Rectangle rectangle = ( Rectangle ) sender;
			RectangleDataModel selRectangle = ( RectangleDataModel ) rectangle.DataContext;

			// setting the selected rectangle to true
			selRectangle.IsSelected = true;

			// for fill tool
			if( CurrentActiveTool == ToolEnum.Fill)
			{
				// checking if the mouse was release on the same selected rectangle
				if(  storedSelectedMouseDownRectangle == rectangle )
				{
					rectangle.Fill = new SolidColorBrush( CurrActiveColor );
				}
				// storing the rectangle to compare to when mouse is up
				storedSelectedMouseDownRectangle = rectangle;
				return;
			}
		}


		/// <summary>
		/// Event raised when mouse is pressed down when the pointer is over a canvas_drawing.
		/// </summary>
		private void canvas_drawing_MouseDown( object sender , MouseButtonEventArgs e )
		{
			// only do its thing if current tool is rectangle tool
			if( mCurrActiveTool != ToolEnum.Rectangle )
			{
				return;
			}

			// checking if the mouse is out of image area
			bool pointerInValidRegion = e.GetPosition( rec_imgBorder ).X > mBorderThickness / 2
				&& e.GetPosition( rec_imgBorder ).X < rec_imgBorder.Width - mBorderThickness / 2
				&& e.GetPosition( rec_imgBorder ).Y > mBorderThickness / 2
				&& e.GetPosition( rec_imgBorder ).Y < rec_imgBorder.Height - mBorderThickness / 2;

			if( !pointerInValidRegion )
			{
				return;
			}

			// save the current postion and initiaing drawing rectangle
			mMouseStartPoint = e.GetPosition(canvas_drawing);
			mDrawingRectangles = true;
		}

		/// <summary>
		/// Event raised when mouse is moved when the pointer is over a canvas_drawing and the mouse is pressed down.
		/// </summary>
		private void canvas_drawing_MouseMove( object sender , MouseEventArgs e )
		{
			// checking if rectangle tool is selected and rectangle is being drawn
			if( mCurrActiveTool == ToolEnum.Rectangle && mDrawingRectangles)
			{
				// save the current postion and initiaing drawing rectangles
				border_drawing.Opacity = 0.75;
				initborder_drawing( mMouseStartPoint , e.GetPosition( canvas_drawing ) );
			}
		}

		/// <summary>
		/// Event raised when mouse is released when the pointer is over a canvas_drawing and mouse button is pressed down.
		/// </summary>
		private void canvas_drawing_MouseUp( object sender , MouseButtonEventArgs e )
		{
			// checking if rectangle tool is selected and rectangle is being drawn
			if( mCurrActiveTool == ToolEnum.Rectangle && mDrawingRectangles )
			{
				// create a rectangle based on border
				RectangleData.RectangleDataInstance.addRectangleToRectangles( new RectangleDataModel( mDrawingRectangleTopleftPos.X , mDrawingRectangleTopleftPos.Y , border_drawing.Width , border_drawing.Height , mCurrActiveColor , false ) );

				// hiding the drawing border and leaving the drawing mode
				border_drawing.Opacity = 0;
				mDrawingRectangles = false;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes the member variables
		/// </summary>
		private void initializeMemberVariables()
		{
			CurrActiveColor = Color.FromArgb( 0xFF , 0x00 , 0x00 , 0x00 );

			CurrentActiveTool = ToolEnum.Hand;

			// foreground and background brushes for button when it is active or inactive
			mActiveButtonFgBrush = Brushes.White;
			mActiveButtonBgBrush = Brushes.Black;
			mInActiveButtonFgBrush = Brushes.Black;
			mInActiveButtonBgBrush = Brushes.LightGray;

			mBorderThickness = 5;
			mDrawingRectangles = false;
			mDragingRectangles = false;
			mImageLoaded = false;
		}

		/// <summary>
		/// Sets the passed button's foreGround and backgorund  to active colors
		/// </summary>
		/// <param name="aButton"> Button. Button to set active</param>
		private void activateToolBtn(Button aButton)
		{
			aButton.Foreground = mActiveButtonFgBrush;
			aButton.Background = mActiveButtonBgBrush;
		}

		/// <summary>
		/// Sets the passed button's foreGround and backgorund  to indicate it is not active colors
		/// </summary>
		/// <param name="aButton"> Button Button to set active</param>
		private void deactivateToolBtn( Button aButton )
		{
			aButton.Foreground = mInActiveButtonFgBrush;
			aButton.Background = mInActiveButtonBgBrush;
		}

		/// <summary>
		/// Gets the reference to the current active tool button
		/// </summary>
		/// <returns>Button. reference to the current active tool button</returns>
		private Button getCurrActiveToolButton()
		{
			if(CurrentActiveTool == ToolEnum.Rectangle)
			{
				return btn_rectangleTool;
			}
			else if ( CurrentActiveTool == ToolEnum.Fill )
			{
				return btn_fillTool;
			}

			return btn_handTool;
		}

		/// <summary>
		/// Update the position and size of the rectangle that user is dragging out.
		/// </summary>
		private void initborder_drawing( Point aOrginalPoint , Point aNewPoint )
		{
			double width;
			double height;

			// Deterine x, y, width and height of the rectangle
			// if the new point is left or top of the orginal point then invert the rectangle
			if( aNewPoint.X < aOrginalPoint.X )
			{
				mDrawingRectangleTopleftPos.X = aNewPoint.X;
				width = aOrginalPoint.X - aNewPoint.X;
			}
			else
			{
				mDrawingRectangleTopleftPos.X = aOrginalPoint.X;
				width = aNewPoint.X - aOrginalPoint.X;
			}

			if( aNewPoint.Y < aOrginalPoint.Y )
			{
				mDrawingRectangleTopleftPos.Y = aNewPoint.Y;
				height = aOrginalPoint.Y - aNewPoint.Y;
			}
			else
			{
				mDrawingRectangleTopleftPos.Y = aOrginalPoint.Y;
				height = aNewPoint.Y - aOrginalPoint.Y;
			}

			// Update the coordinates of the rectangle that is being dragged out by the user.
			Canvas.SetLeft( border_drawing , mDrawingRectangleTopleftPos.X );
			Canvas.SetTop( border_drawing , mDrawingRectangleTopleftPos.Y );
			border_drawing.Width = width;
			border_drawing.Height = height;
		}

		#endregion

		#region Private Data Members

		/// <summary>
		/// holds the current selected color
		/// </summary>
		private Color mCurrActiveColor;

		/// <summary>
		/// current active tool
		/// </summary>
		private ToolEnum mCurrActiveTool;

		// foreground and background brushes for button when it is active or inactive
		private Brush mActiveButtonFgBrush;
		private Brush mActiveButtonBgBrush;
		private Brush mInActiveButtonFgBrush;
		private Brush mInActiveButtonBgBrush;

		/// <summary>
		/// holds the image uploaded by user
		/// </summary>
		private BitmapImage mBgImage;

		/// <summary>
		/// Thickness of the border
		/// </summary>
		private double mBorderThickness;

		/// <summary>
		/// true if a rectangle is being drawn
		/// </summary>
		private bool mDrawingRectangles;

		/// <summary>
		/// true if a rectangle is being dragged
		/// </summary>
		private bool mDragingRectangles;

		/// <summary>
		/// true if image is loaded in the canvas
		/// </summary>
		private bool mImageLoaded;

		/// <summary>
		/// starting  position of the mouse
		/// </summary>
		private Point mMouseStartPoint;

		/// <summary>
		/// stores position of drawing rectangle
		/// </summary>
		private Point mDrawingRectangleTopleftPos;

		/// <summary>
		/// stores the selected rectangle when mouse is pressed down
		/// </summary>
		private Rectangle storedSelectedMouseDownRectangle;

		#endregion
	}
}
