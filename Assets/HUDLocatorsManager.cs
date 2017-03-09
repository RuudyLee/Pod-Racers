using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDLocatorsManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static HUDLocatorsManager _manager;
    public static HUDLocatorsManager manager
    {
        get
        {
            if (_manager == null)
            {
                _manager = GameObject.FindObjectOfType<HUDLocatorsManager>();
            }
            return _manager;
        }
    }

    [Header("The camera frustrum")]
    public Camera eyeCmaera;

    [Header("The things to draw")]
    public List<Transform> m_transformsToDraw;
    private Dictionary<Transform, RectTransform> m_transformLocators;

    public GameObject templateOnScreenLocator;
    public GameObject templateOffScreenLocator;

    void Start()
    {
        m_transformLocators = new Dictionary<Transform, RectTransform>();

        templateOffScreenLocator.SetActive(false);
        templateOnScreenLocator.SetActive(false);
    }

    void Update()
    {
        foreach (Transform t in m_transformsToDraw)
        {
            // Check if it's in the dictionary
            if (!m_transformLocators.ContainsKey(t))
            {
                GameObject child = new GameObject(t.name + "_LOCATOR", new System.Type[] { typeof(RectTransform) });

                var rt = child.transform as RectTransform;

                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;

                GameObject c1 = Instantiate(templateOnScreenLocator, Vector3.zero, Quaternion.identity, child.transform);
                GameObject c2 = Instantiate(templateOffScreenLocator, Vector3.zero, Quaternion.identity, child.transform);

                c1.name = "OnScreen";
                c2.name = "OffScreen";

                m_transformLocators.Add(t, child.transform as RectTransform);

                child.transform.SetParent(transform, false);
            }

            {
                var rt1 = m_transformLocators[t].FindChild("OnScreen") as RectTransform;
                rt1.gameObject.SetActive(false);
                var rt2 = m_transformLocators[t].FindChild("OffScreen") as RectTransform;
                rt2.gameObject.SetActive(false);
            }

            // Check if it is in the view frustrum
            Matrix4x4 mvp = eyeCmaera.projectionMatrix *
                eyeCmaera.worldToCameraMatrix;

            // Check the point
            Vector3 point = mvp.MultiplyPoint(t.position);
            Debug.Log("Point: " + point.ToString());

            // if it's in the box
            if (Mathf.Abs(point.x) < 1.0f &&
                Mathf.Abs(point.y) < 1.0f &&
                Mathf.Abs(point.z) < 1.0f)
            {
                // It's in the box, so we can see it
                Debug.Log("OnScreen");
                DrawOnScreen(t, point);
            }
            else
            {
                Debug.Log("OffScreen");
                DrawOffScreen(t, point);
            }
        }
    }

    void DrawOnScreen(Transform originalPoint, Vector3 positionToDraw)
    {
        var rt = m_transformLocators[originalPoint].FindChild("OnScreen") as RectTransform;
        rt.gameObject.SetActive(true);

        Vector2 anchorPos = new Vector2(positionToDraw.x * 0.5f + 0.5f,
            positionToDraw.y * 0.5f + 0.5f);

        rt.anchorMin = rt.anchorMax = anchorPos;
    }

    void DrawOffScreen(Transform originalPoint, Vector3 positionToDraw)
    {
        float dot = Vector3.Dot(Vector3.Normalize(originalPoint.position - eyeCmaera.transform.position), 
            eyeCmaera.transform.forward);

        var rt = m_transformLocators[originalPoint].FindChild("OffScreen") as RectTransform;
        rt.gameObject.SetActive(true);

        positionToDraw = positionToDraw * dot;

        Vector2 anchorPos = new Vector2(positionToDraw.x,
            positionToDraw.y).normalized * Mathf.Sqrt(2.0f);

        anchorPos.x = Mathf.Clamp(anchorPos.x, -1.0f, 1.0f);
        anchorPos.y = Mathf.Clamp(anchorPos.y, -1.0f, 1.0f);

        anchorPos = new Vector2(Mathf.Clamp(anchorPos.x * 0.5f + 0.5f, 0.0f, 1.0f),
            Mathf.Clamp(anchorPos.y * 0.5f + 0.5f, 0.0f, 1.0f));

        rt.anchorMin = rt.anchorMax = anchorPos;
    }
}
