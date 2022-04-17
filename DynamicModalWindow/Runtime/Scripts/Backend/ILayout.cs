using System.Collections.Generic;
using UnityEngine;


namespace GameSystems.Popup.Backend
{
    /// <summary>
    /// Base Interface for all Layouts
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// Interface method for getting all elements. added to this layout.
        /// </summary>
        /// <returns>The list of the elements.</returns>
        List<LayoutElement> GetElements();

        /// <summary>
        /// Get the total Width that is required to fill all the elements in the popup.
        /// </summary>
        float GetTotalWidth();

        /// <summary>
        /// Get the total height that is required to fill all the elements in the popup.
        /// </summary>
        float GetTotalHeight();

        /// <summary>
        /// Append the given element to the layout.
        /// </summary>
        /// <param name="element"> The element to add to the layout </param>
        /// <returns> The position of the element </returns>
        Vector2 AddElement(IPopupElement element);
    }
}