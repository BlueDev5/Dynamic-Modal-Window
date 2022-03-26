# Documentation

## Overview

### PopupManager

Singleton class for Showing and hiding popups.

Inherits: UnityEngine.MonoBehaviour

### PopupAction

Represents a action for the PopupButton.

Inherits: System.Object

Constructor(s):

```csharp
new PopupAction(string buttonText, Action clickCallback, Color backgroundColor);
```

### PopupRequest

The request for popup.

Inherits: System.Object

Constructor(s):

```csharp
new PopupRequest(List<IPopupElement> elements, List<PopupAction> actions, ILayout layout, string title = "");
```

### ILayout

Base Interface for all layouts for the popup.

### IPopupElement

Base Interface for all popupElements.

### PopupElement

A simple invisible element (like a divider)

Inherits: System.Object, IPopupElement

Constructor(s):

```csharp
new PopupElement(float preferredWidth, float preferredHeight);
```

#### Text Element

A popup Element with a text on it

Inherits: System.Object, IPopupElement

Constructor(s):

```csharp
new TextElement(string text, float preferredWidth, float preferredHeight);
```

### Sprite Element

A popup Element with a Sprite.

Inherits: System.Object, IPopupElement

Constructor(s):

```csharp
new SpriteElement(float preferredWidth, float preferredHeight, Sprite texture2D, Color? tint = null);
```

### Tall Layout

A Layout With 2 columns (first one containing only 1 element and second one being a vertical layout with unlimited elements)

Inherits: System.Object, ILayout

Constructor(s):

```csharp
new TallLayout(Vector4 margin);
```

Illustration:

<pre>
---------------------
|            |      | ----
|            |______|     |
|            |      |     \
|            |      |      => The amount of rows on the 2nd column is variable.
|            |______|     /
|            |      |     |
|            |      | ----
---------------------
</pre>

### Wide Layout

A Layout With 2 rows (first one containing only 1 element and second one being a horizontal layout with unlimited elements)

Inherits: System.Object, ILayout

Constructor(s):

```csharp
new WideLayout(Vector4 margin);
```

Illustration:

<pre>
---------------------
|                   |
|                   |
|___________________|
|     |       |     | 
|     |       |     |  => The amount of columns on the second row is variable.
|     |       |     | 
---------------------
</pre>

### Grid Layout

A Layout With variable rows and columns.

Inherits: System.Object, ILayout

Constructor(s):

```csharp
public GridLayout(Vector4 margin, Vector2 spacing, int maxRows = 1, int maxColumns = 1)
```

Illustration:

<pre>
---------------------
|         |         |
|         |         | --
|___________________|   |
|         |         |    --> The number of rows is variable.
|         |         |   |
|         |         | --
---------------------
    |         |
     ---- ----    
         |
  The number of columns is also variable.    
</pre>

## Showing a popup

### Step 1: Creating Popup Request

First of all to show an popup you need a popup Request. (Remember to add using Gamesystems.Popup.Backend)
Here's the Syntax:

```csharp
var popupRequest = new Popup(List<IPopupElement> elements, List<PopupAction> actions, ILayout layout, string title = "");
```

where elements are the elements of the popup, actions are the buttons to display with the function they'll execute, layout is the ILayout class that tells the popup manager where to place the next window and title is the title of the popup (default is "");

For Example:

```csharp
  // The elements for the popup.
  var elements = new List<IPopupElement>()
  {
      new TextElement("This Is some information.", 300, 150),
      new TextElement("Secondary Information.", 200, 100),
      new SpriteElement(200, 100, _texture, _tint),
  };

  // The buttons of the popup.
  var actions = new List<PopupAction>()
  {
      new PopupAction("Yes", () => Debug.Log("Yes"), Color.green),
      new PopupAction("No", () => Debug.Log("No"), Color.red),
  };

  // The layout to use
  var layout = new WideLayout();

  // The request we create.
  PopupRequest request = new PopupRequest(elements, actions, layout, "Hello");
```

### Step 2: Passing the request

Passing the request to popupManager is really simple:

```csharp
PopupManager.Instance.ShowPopup(request);
```

where request is the Popup request you wanna send.

### Step 3: Hiding the popup

You can just do:
```csharp
PopupManager.HidePopup();
```

## Miscellaneous Tips

### Creating complex Popup Requests

Let's say you want to create a popup request that yields this result:

<pre>
---------------------
|      |     |      |
|      |     |      |
|______|_____|______|
|         |         |
|         |         |
|_________|_________|
|                   |
|                   |
---------------------
</pre>

For this the request would look something like this:

```csharp
  // The buttons of the popup.
  var actions = new List<PopupAction>()
  {
    //add actions if you want any
  };

  // The layout to use. This is a 3 by 3 Grid Layout
  var layout = new GridLayout(margin: new Vector4(10, 10, 10, 10), spacing: new Vector2(5, 5), maxRows: 3, maxColumns: 3);

  // The elements for the popup.
  var elements = new List<IPopupElement>()
  {
    // First Row //
    new PopupElement(100, 150),
    new PopupElement(100, 100),
    new PopupElement(100, 100),
    // First Row //

    // Second Row //
    new PopupElement(150, 100),
    new PopupElement(150, 100),
    new PopupElement(0, 0),
    // Second Row //

    // Third Row //
    new PopupElement(300, 100),
    // Third Row //
  };

  // The request we create.
  PopupRequest request = new PopupRequest(elements, actions, layout, "Hello");
```

Notice, I used an empty popup element with 0 width and 0 height. This will ensure that the next element is placed on next row, If it weren't there the system will think that the element that belonged to third row is the last element of the 2nd row.