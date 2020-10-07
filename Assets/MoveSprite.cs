using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    // VARIABLES
    private Vector3 mouse_start_pos;
    private float move_thresh = 50;  // Has to be at least 50 pixels different from starting position to count.
    private bool dragging = false;
    public GameObject arrow_line; // This is the line renderer option

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseDown()
    {
        // User clicked on the collider box
        dragging = true;
        mouse_start_pos = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // Get position and see if we are above, below, left, or right of starting pos
        Vector3 mouse_end_pos = Input.mousePosition;
        Debug.Log("ENDING AT: " + mouse_end_pos);
        if (mouse_end_pos.x > mouse_start_pos.x + move_thresh)
        {
            Debug.Log("MOVE RIGHT");
        }
        else if (mouse_end_pos.x < mouse_start_pos.x - move_thresh)
        {
            Debug.Log("MOVE LEFT");
        }
        else
        {
            Debug.Log("NO HORIZONTAL MOVEMENT");
        }

        if (mouse_end_pos.y > mouse_start_pos.y + move_thresh)
        {
            Debug.Log("MOVE UP");
        }
        else if (mouse_end_pos.y < mouse_start_pos.y - move_thresh)
        {
            Debug.Log("MOVE DOWN");
        }
        else
        {
            Debug.Log("NO VERTICAL MOVEMENT");
        }

        // Turn off the mesh visibility
        //arrow.enabled = false;
        dragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 current_position = Input.mousePosition;

            // This will be a try at rotating the arrow_line object
            Vector3 world_point = Camera.main.ScreenToWorldPoint(current_position);

            if ((current_position - mouse_start_pos).magnitude < move_thresh)
            {
                // Turn off the arrow - won't show position unless we're moving
                arrow_line.SetActive(false);
            }
            else
            {
                // Turn the arrow on and calculate its angle
                arrow_line.SetActive(true);
                float angle = Mathf.Atan(world_point.y/world_point.x) * 180 / Mathf.PI;
                arrow_line.transform.eulerAngles = new Vector3(0, 0, angle);

                // Need to flip the arrow since arctan will only ever give me -90 to +90
                if (world_point.x < 0)
                {
                    arrow_line.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    arrow_line.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
