# RectanglesOnImage_WPF_App

## Introduction
An WPF application that allows the users to create and manipulate rectangles over an image. It allows user to load images, create rectangles over the image, drag the created rectangles, change color of the rectangle, delete the created rectangles, and save the image. 

## Implemented Features
This application allows the uses to perform following task:
- Select and load an image.
- Draw one rectangle or more over the image by clicking and dragging the mouse to draw (the size of the rectangle depends on how much the user drags the mouse)
- Only allow drawing inside the picture.
- Resize the rectangle(s) from any corner or side.
- Move the rectangle by pressing and holding the rectangle and moving themouse (drag and drop).
- Delete any rectangle.
- Save the changes to a new image.

## Missing Features
- Change the rectangle's color by clicking each rectangle and selecting a different color from a color palette.
- The user can open the image using your application and update the shapes that were drawn in a previous session.

## Knows Issues
- Saved image resolution is lower than the uploaded image.
- The newly created rectangles are little bit less accurate in their positioning when they are created. They have a tendency to shift down by a little both in x and y aixs

## How to use it.
As mentioned before, this application supports loading images, creating rectangles over the loaded image, drag the created rectangles, change color of the rectangle, delete the created rectangles, and save the image.

A toolbar is docked on the top of the window with all the button ui element. Starting from the left hand, this toolbar has 2 buttons. First one is a load image button that pops up a file dialouge box to let the user to select the image file to load into the canvas. Immediately following  load image button is save image button that lets image in to the canvas and save the edited image.

In the middle, is where the current color selection is displayed. when hand tool is selected and a rectangle is selected, a button to delete the selected rectangle also appeared here. 

Folowing that, is color palette. It is a group of 5 button that lets the users set the active color. After that are the 3 tools, namely, Fill Tool, Rect Tool, and Hand tool.

The following image shows the landing window whit the tools discussed above and a canvas where the image can be loaded.

![image](https://user-images.githubusercontent.com/68620206/219828305-654e064d-262c-4baf-a1aa-d2ceb60f35ae.png)

This image shows when a image is loaded to the canvas.

![image](https://user-images.githubusercontent.com/68620206/219828360-ece3c73c-4313-4836-8f18-aa198833f6dd.png)

### Tools 
#### Rect Tool
Rect Tool allows the users to create rectangles by pressing and draging the mouse over the canvas, and releasing the mouse button. User can selects any colors, and the rectanlge that were created after will have the selected fill color 

Following figure shows the rectangle created with different colors, with the help of the rect tool.

![image](https://user-images.githubusercontent.com/68620206/219829585-39d79d52-9983-438d-b094-17acff453efd.png)

#### Fill Tool
Fill Tool allows the users to color the already created rectangle with a diffent color.

Following image almost similar to the above image, and only difference being that colors are changed.

![image](https://user-images.githubusercontent.com/68620206/219829675-0c30a0f6-3763-4fe8-afac-e42f28e28f42.png)

#### Hand Tool
Hand Tool allows the uses to select any created rectangles and drag them across the canvas. 

The following image shows that the rectangle were moved form thire orginal positions.

![image](https://user-images.githubusercontent.com/68620206/219829765-f4ed8118-75b6-4d4a-a487-820134707d3b.png)

Further more when a created rectangle is selected, a delete button becomes avialabe which allows uses to delete the selected rectangle. In the above image the green rectangle is selcted indicated by the blue outline around the green rectangle. And we can also see that the delete button appears when a rectangle is selcted.

In the following image we can see that the green rectangle was deleted. Also notice that the delete button is hidden as no rectangle is selected. 

![image](https://user-images.githubusercontent.com/68620206/219829791-be192263-7d4b-49bd-8cdd-c6347c0ff1fc.png)

 Finally, when the applicaiton is first opened, the hand tool is selected by default.

## Reference
- Davis, Ashley. *“A WPF Custom Control for Zooming and Panning.”* CodeProject, 04 Jun. 2010, hhttps://www.codeproject.com/Articles/85603/A-WPF-custom-control-for-zooming-and-panning.
- Davis, Ashley. *“A Simple Technique for Data-binding to the Position of a UI Element in WPF.”* CodeProject, 23 Dec. 2010, https://www.codeproject.com/Articles/139216/A-Simple-Technique-for-Data-binding-to-the-Positio.
- Davis, Ashley. *“Simple Drag Selection in WPF”* CodeProject, 20 Jan. 2011, https://www.codeproject.com/Articles/148503/Simple-Drag-Selection-in-WPF.
- Microsoft Corporation. *"Desktop Guide (WPF .NET)."* Microsoft documentation. https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-7.0
- Smith, Josh. *“A Guided Tour of WPF.”* CodeProject, 19 Apr. 2007, https://www.codeproject.com/Articles/18232/A-Guided-Tour-of-WPF-Part-1-XAML.
- Alex, Sam. *"How do I set background image to a Canvas."* Stackoverflow, 15 Apr. 2015, https://stackoverflow.com/questions/29650471/how-do-i-set-background-image-to-a-canvas/29651843#29651843.
    - Answered by Memory of a dream. https://stackoverflow.com/a/29651843
- Jonlin. *"How to bind code behind variables in WPF."* CodeProject, 29 Apr. 2016, https://www.codeproject.com/Questions/1096881/How-to-bind-code-behind-variables-in-WPF.
    - Answered by Heffron, Matt T. https://www.codeproject.com/Answers/1096888/How-to-bind-code-behind-variables-in-WPF
- zc21. *"Save canvas to bitmap."* Stackoverflow, 01 May. 2011, https://stackoverflow.com/questions/5851168/save-canvas-to-bitmap.
    - Answered by Icemanind. https://stackoverflow.com/a/12378707
