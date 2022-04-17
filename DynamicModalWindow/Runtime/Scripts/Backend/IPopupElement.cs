using UnityEngine;

namespace GameSystems.Popup.Backend
{
    /// <summary>
    /// Base Interface for all popup elements.
    /// </summary>
    public interface IPopupElement
    {
        /// <summary>
        /// The width of this Element.
        /// </summary>
        float Width { get; }

        /// <summary>
        /// The height of this Element.
        /// </summary>
        float Height { get; }

        /// <summary>
        /// The Parent of this element. If null the bodyRoot of the Popup Canvas is used
        /// </summary>
        Transform Parent { get; set; }

        /// <summary>
        /// Instantiate this Popup Element.
        /// </summary>
        /// <param name="position"> The position at which the element has to be instantiated.</param>
        /// <returns> The created Element's root. </returns>
        GameObject Create(Vector2 position);
    }
}