using System.Collections.Generic;
using UnityEngine;


namespace GameSystems.Popup.Backend
{
    public interface ILayout
    {
        List<LayoutElement> GetElements();

        float GetTotalWidth();
        float GetTotalHeight();

        Vector2 AddElement(IPopupElement element);
    }
}