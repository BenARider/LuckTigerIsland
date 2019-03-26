using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Menu
{
    // Detects if an immediate child is selected and out of the scroll view. If so it auto-scrolls everything into view.
    public class AutoScroll : MonoBehaviour
    {
        [SerializeField] bool debug;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] Scrollbar scrollbar;
        [SerializeField] float scrollPadding = 20f;

        void Start()
        {
            StartCoroutine(DetectScroll());
        }

        IEnumerator DetectScroll()
        {
            GameObject current;
            GameObject prevGo = null;
            Rect currentRect = new Rect();
            Rect viewRect = new Rect();
            RectTransform view = scrollRect.GetComponent<RectTransform>();

            while (true)
            {
                current = EventSystem.current.currentSelectedGameObject;
                if (current != null && current.transform.parent == transform)
                {
                    // Get a cached instance of the RectTransform
                    if (current != prevGo)
                    {
                        RectTransform rt = current.GetComponent<RectTransform>();

                        // Create rectangles for comparison
                        currentRect = GetRect(current.transform.position, rt.rect, Vector2.zero);
                        viewRect = GetRect(scrollRect.transform.position, view.rect, view.offsetMax);
                        Vector2 heading = currentRect.center - viewRect.center;

                        if (heading.y > 0f && !viewRect.Contains(currentRect.max))
                        {
                            float distance = Mathf.Abs(currentRect.max.y - viewRect.max.y) + scrollPadding;
                            view.anchoredPosition = new Vector2(view.anchoredPosition.x, view.anchoredPosition.y - distance);
                            if (debug) Debug.LogFormat("Scroll up {0}", distance); // Decrease y value
                        }
                        else if (heading.y < 0f && !viewRect.Contains(currentRect.min))
                        {
                            float distance = Mathf.Abs(currentRect.min.y - viewRect.min.y) + scrollPadding;
                            view.anchoredPosition = new Vector2(view.anchoredPosition.x, view.anchoredPosition.y + distance);
                            if (debug) Debug.LogFormat("Scroll down {0}", distance); // Increase y value
                        }

                        // Get adjusted rectangle positions
                        currentRect = GetRect(current.transform.position, rt.rect, Vector2.zero);
                        viewRect = GetRect(scrollRect.transform.position, view.rect, view.offsetMax);
                    }
                }

                prevGo = current;

                if (debug)
                {
                    DrawBoundary(viewRect, Color.cyan);
                    DrawBoundary(currentRect, Color.green);
                }

                yield return null;
            }
        }

        static Rect GetRect(Vector3 pos, Rect rect, Vector2 offset)
        {
            float x = pos.x + rect.xMin - offset.x;
            float y = pos.y + rect.yMin - offset.y;
            Vector2 xy = new Vector2(x, y);

            return new Rect(xy, rect.size);
        }

        public static void DrawBoundary(Rect rect, Color color)
        {
            Vector2 topLeft = new Vector2(rect.xMin, rect.yMax);
            Vector2 bottomRight = new Vector2(rect.xMax, rect.yMin);

            Debug.DrawLine(rect.min, topLeft, color); // Top
            Debug.DrawLine(rect.max, topLeft, color); // Left
            Debug.DrawLine(rect.min, bottomRight, color); // Bottom
            Debug.DrawLine(rect.max, bottomRight, color); // Right
        }
    }
}