using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesOnImage_WPF_App
{
	/// <summary>
	/// Collection of rectangles that are drawn in the canvas
	/// </summary>
	class RectangleData: INotifyPropertyChanged
	{

		#region Public Properties

		/// <summary>
		/// The list of rectangles that is displayed both in the main window and in the overview window.
		/// </summary>
		public ObservableCollection<RectangleDataModel> Rectangles
		{
			get
			{
				return mRectangles;
			}
		}

		/// <summary>
		/// Retreive the singleton instance.
		/// </summary>
		public static RectangleData RectangleDataInstance
		{
			get
			{
				return mRectangleDataInstance;
			}
		}

		#endregion

		#region Public Constructor

		/// <summary>
		/// Default Constructor. 
		/// </summary>
		public RectangleData()
		{
			mRectangles = new ObservableCollection<RectangleDataModel>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Add Rectangle to the rectangles
		/// </summary>
		/// <param name="aRectangle">RectangleDataModel. Rectangle to add to the data list</param>
		public void addRectangleToRectangles( RectangleDataModel aRectangle)
		{
			mRectangles.Add( aRectangle );
		}

		/// <summary>
		/// Removes Rectangle to the rectangles
		/// </summary>
		/// <param name="aRectangle">RectangleDataModel. Rectangle to add to the data list</param>
		public void removeRectangleToRectangles( RectangleDataModel aRectangle )
		{
			mRectangles.Remove( aRectangle );
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
		/// The list of rectangles.
		/// </summary>
		private ObservableCollection<RectanglesOnImage_WPF_App.RectangleDataModel> mRectangles;

		/// <summary>
		/// instance of rectangleData
		/// </summary>
		private static RectangleData mRectangleDataInstance = new RectangleData();

		
		#endregion
	}
}
