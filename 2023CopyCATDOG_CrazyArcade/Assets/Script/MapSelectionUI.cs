using UnityEngine;
using UnityEngine.UI;
public class MapSelectionUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevMap;
    public Transform selectedMap;

    private void Start()
    {
        foreach (Map c in GameManager.Inst.selectmap)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                GameManager.Inst.SetMap(c);
                if (selectedMap != null)
                {
                    prevMap = selectedMap;
                }

                selectedMap = option.transform;
            });

            Text text = option.GetComponentInChildren<Text>();
            text.text = c.name;

            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.Icon;

        }
    }

    private void Update()
    {
        if (selectedMap != null)
        {
            selectedMap.localScale = Vector3.Lerp(selectedMap.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime);
        }
        if (prevMap != null)
        {
            prevMap.localScale = Vector3.Lerp(prevMap.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime);
        }
    }
}
