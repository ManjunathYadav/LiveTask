using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageGrid : MonoBehaviour
{
    public GameObject imagePrefab;
    public int imageWidth = 100;
    public int imageHeight = 100;
    public float padding = 5f;

    void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        int columns = screenWidth / (imageWidth + (int)padding);
        int rows = screenHeight / (imageHeight + (int)padding);

        float startX = 50;
        float startY = 50;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int tempX = x;
                int tempY = y;
                GameObject newImage = Instantiate(imagePrefab, transform) as GameObject;
                Image image = newImage.GetComponent<Image>();
                Button btn = newImage.GetComponent<Button>();
                btn.onClick.AddListener(() => ButtonClicked(tempX, tempY, image));
                RectTransform rectTransform = newImage.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector3((startX + (y * 100) + ((y + 1) * padding)), -(startY + (x * 100) + ((x + 1) * padding)), 0);
                //Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
                gridImages.Add(new Vector2Int(x, y), image);
            }
        }
    }

    private Dictionary<Vector2Int, Image> gridImages = new Dictionary<Vector2Int, Image>();

    private UnityAction ButtonClicked(int x, int y, Image buttonClicked)
    {
        Debug.Log(x + " " + y);
        Vector2Int clickedPos = new Vector2Int(x, y);
        buttonClicked.color = Color.black;

        // Check all neighboring positions including diagonals
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (gridImages.ContainsKey(pos))
                {
                    Image image = gridImages[pos];
                    if (pos != clickedPos)
                    {
                        image.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                    }
                }
            }
        }

        return null;
    }
}
