using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject m_cam;
    [SerializeField] private GameObject m_bullet;

    [Header("Target & Waypoint")]
    [SerializeField] private Image m_img;
    [SerializeField] private GameObject m_target;
    [SerializeField] private List<ScriptableObject> m_targetRegions;
    [SerializeField] private GameObject m_uiElement;
    [SerializeField] private float m_uiOffset;
    [SerializeField] private TextMeshProUGUI m_uiText;

    [Header("Random Spawn Location")]
    [SerializeField] private bool m_randomizeLocation;
    [SerializeField] private Vector3 m_spawnBounds;

    [Header("Debugging")]
    [SerializeField] private Vector3 m_gizmosSize;
    [SerializeField] private Vector3 m_gizmosPos;

    [SerializeField] private Vector2 pos;

    /// <Get Set Postion>
    /// Kanker mogool - justin
    /// 
    /// Gets the target position
    /// Sets the target position
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return m_target.transform.position;
        }

        set
        {
            m_target.transform.position = value;
        }
    }

    public List<ScriptableObject> GetTargetRegions()
    {
        return m_targetRegions;
    }

    private void Start()
    {
        m_target = GetComponent<Target>().gameObject;
        m_cam = FindObjectOfType<Camera>().gameObject;
    }

    private void Update()
    {
        UpdateWaypoint();
        UpdateUIDistance();
    }

    /// <Update Waypoint *>
    /// Updates the UI image location of the target
    /// 
    /// float minX is the image divided by half. Needed so the image can be perfectly centered
    /// float maxX is the Screenwidth minus minX. 
    /// Max X refers to the heighest point on a 2D plane it should be able to reach.
    /// Min Y refers to the lowest / smallest point it should be visible on.
    /// 
    /// float minY is the image divided by half. Takes the UI Element image and divides it by half so it can only be on the screens centered
    /// float maxY is the Screen Height minus minX. 
    /// Max X refers to the heighest point on a 2D plane it should be able to reach.
    /// Min Y refers to the lowest / smallest point it should be visible on.
    /// 
    /// Then in a local var it takes the Target's position and that of the Camera.
    /// it then checks if the game and bullet are still on / Alive and Uses the Dot function between to get a number between 0 and 1 
    /// To see if the Camera is in a certain direction from the Target
    /// 
    /// set's the Pos Variable to be that of the Camera World screen point.
    /// Adds the Offset to it's position so it sits correctly above the target.
    /// and sets the UI Element/Image to be the new Pos
    /// 
    /// Not sure why it needs to be in a totally different else if with the same statement but it only works like this :( 
    /// Then checks if the pos.x is less than screen width and if it is it set's the Pos X to the maxX variable made earlier
    /// Else it sets the pos.x to be the minX variable made earlier.
    /// 
    /// It finally Clamps both the X and Y pos to not be outside of the Screen.
    /// And then sets the UI Element/Image to the new pos once last time :D (This took me way too long Last updated 08/04/23 05:05) 
    /// </summary>
    /// 
    private void UpdateWaypoint()
    {
        float minX = m_img.GetPixelAdjustedRect().width / 2; 
        float maxX = Screen.width - minX;

        float minY = m_img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        var dir = (transform.position - Camera.main.transform.position).normalized;

        if (Vector3.Dot(dir, Camera.main.transform.forward) > 0) //(GameManager.Instance.m_gameOn && m_bullet.activeSelf
        {
            pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.y += m_uiOffset;
            m_uiElement.gameObject.transform.position = pos;
        }

        else if (Vector3.Dot(dir, Camera.main.transform.forward) > 0) //GameManager.Instance.m_gameOn && m_bullet.activeSelf
        {
            if (pos.x < Screen.width) pos.x = maxX;
            else pos.x = minX;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        m_uiElement.gameObject.transform.position = pos;
    }

    /// <Update Distance>
    /// creates a local float Variable distance and sets it to the distance between the Camera and the target
    /// it then sets the UI Text Element to be equal to distance variable 
    /// </summary>
    private void UpdateUIDistance()
    {
        float dist = Vector3.Distance(m_cam.transform.position, m_target.transform.position);
        m_uiText.text = dist.ToString("0") + " m";
    }

    /// <OnDrawGizmos>
    /// It draws a wirecube box to show the spawn / Random positions of the Target
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(m_gizmosPos, m_gizmosSize);
    }
}
