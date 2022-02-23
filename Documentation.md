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

Base class for all layouts for the popup.

### IPopupElement

Base Class for all popupElements.

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
new TallLayout();
```

Illustration:

---

---

### Wide Layout

A Layout With 2 rows (first one containing only 1 element and second one being a horizontal layout with unlimited elements)

Inherits: System.Object, ILayout

Constructor(s):

```csharp
new WideLayout();
```

## Showing a popup

### Step 1: Creating Popup Request

First of all to show an popup you need a popup Request. (Remember to add using Gamesystems.Popup.Backend)
Here's the Syntax:

`var popupRequest = new Popup(List<IPopupElement> elements, List<PopupAction> actions, ILayout layout, string title = "");`

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
