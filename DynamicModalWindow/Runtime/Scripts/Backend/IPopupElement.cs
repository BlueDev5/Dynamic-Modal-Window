using UnityEngine;

namespace GameSystems.Popup.Backend
{
    public interface IPopupElement
    {
        float Width { get; }
        float Height { get; }
        Transform Parent { get; set; }

        GameObject Create(Vector2 position, GameObject parent);
    }
}