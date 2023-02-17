using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
		
		public Color CurrActiveColor
		{
			set
			{
				mCurrActiveColor = value;
			}
			get
			{
				return mCurrActiveColor;
			}
		}

		private ToolEnum CurrentActiveTool
		{
			set
			{
				if ( mCurrActiveTool == value)
				{
					return;
				}

				// deactive curr tool, save new tool, activate new tool
				deactivateToolBtn( getCurrActiveToolButton());
				mCurrActiveTool = value;
				activateToolBtn( getCurrActiveToolButton());
			}
			get
			{
				return mCurrActiveTool;
			}
		}

		#endregion

		public MainWindow()
		{
			InitializeComponent();
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

		# region Private Event

		/// <summary>
		/// Event raised when load image button is clicked. Allows user to load a image file to canvas.
		/// </summary>
		private void btn_loadImg_Click( object sender , RoutedEventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image|*.jpg;*.jpeg;*png";

			if( openFileDialog.ShowDialog() == true)
			{
				BitmapImage bgImage = new BitmapImage( new Uri( ( string ) openFileDialog.FileName ) );
				bgImageBrush.ImageSource = bgImage;
				//btm.Save( openFileDialog.FileName , ImageFormat.Jpeg );
				//MessageBox.Show( "Image Saved Successfully..." );
			}
		}

		/// <summary>
		/// Event raised when save image button is clicked. Allows user to save the canvas as image file.
		/// </summary>
		private void btn_SaveImg_Click( object sender , RoutedEventArgs e )
		{

		}

		/// <summary>
		/// Event raised when color tool button is clicked. Allows user to set the color of the rectangle.
		/// </summary>
		private void btn_colorPicker_Click( object sender , RoutedEventArgs e )
		{

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
			CurrentActiveTool = ToolEnum.Rectangle;
		}

		/// <summary>
		/// Event raised when hand tool button is clicked. Sets hand tool as active tool.
		/// </summary>
		private void btn_handTool_Click( object sender , RoutedEventArgs e )
		{
			CurrentActiveTool = ToolEnum.Hand;
		}

		#endregion

		#region Private Methods

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

		#endregion

		#region Private Data Members

		/// <summary>
		/// holds the current selected color
		/// </summary>
		private Color mCurrActiveColor = Color.FromArgb( 0xFF, 0x00, 0x00, 0x00);

		/// <summary>
		/// current active tool
		/// </summary>
		private ToolEnum mCurrActiveTool = ToolEnum.Hand;

		// foreground and background brushes for button when it is active or inactive
		private Brush mActiveButtonFgBrush = Brushes.White;
		private Brush mActiveButtonBgBrush = Brushes.Black;
		private Brush mInActiveButtonFgBrush = Brushes.Black;
		private Brush mInActiveButtonBgBrush = Brushes.LightGray;

		#endregion

	}
}
