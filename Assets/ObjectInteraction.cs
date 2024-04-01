using TMPro;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f; // Расстояние для взаимодействия с объектом
    [SerializeField] private LayerMask interactableLayer; // Слой для объектов, с которыми можно взаимодействовать
    [SerializeField] private GameObject interactionTextPrefab; // Префаб текста для отображения буквы

    private Camera mainCamera;

    private GameObject currentInteractableObject; // Текущий объект, с которым можно взаимодействовать
    private GameObject interactionTextInstance; // Экземпляр текста для отображения буквы
    private GameObject player; // Экземпляр текста для отображения буквы

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Получаем объект, с которым мы можем взаимодействовать
            GameObject interactableObject = hit.collider.gameObject;
            if (interactionTextInstance != null)
            {
                interactionTextInstance.transform.rotation = player.transform.rotation;
            }

            if (interactableObject != currentInteractableObject)
            {
                currentInteractableObject = interactableObject;

                // Отображаем букву рядом с объектом
                ShowInteractionText(interactableObject);
                
                
            }

            if (Input.GetKey(KeyCode.E))
            {
                GameManager.Instanсe.ShowCardOne();
            }
        }
        else
        {
            // Если луч не попадает на объект, скрываем текст
            HideInteractionText();
            currentInteractableObject = null;
        }
    }

    private void ShowInteractionText(GameObject interactableObject)
    {
        // Создаем экземпляр текста
        if (interactionTextInstance == null)
        {
            interactionTextInstance = Instantiate(interactionTextPrefab, transform);
        }

        // Позиционируем текст над объектом
        interactionTextInstance.transform.position = interactableObject.transform.position + Vector3.down * 0.5f + Vector3.back * 0.35f;
        
        // Устанавливаем текст в зависимости от клавиши взаимодействия
        string interactionKey = "E"; // Пример: клавиша "E"
        interactionTextInstance.GetComponent<TMP_Text>().text = interactionKey;
    }

    private void HideInteractionText()
    {
        // Уничтожаем экземпляр текста
        if (interactionTextInstance != null)
        {
            Destroy(interactionTextInstance);
        }
    }
}
