using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class colision : MonoBehaviour
{
    private bool hasItem = false; // Indique si le cube contient déjà un élément
    private Vector3 originalScale; // Stocke l'échelle d'origine de l'élément

    // Start is called avant la première mise à jour du cadre
    void Start()
    {
        
    }

    // Update est appelé une fois par cadre
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasItem) // Vérifie si le cube contient déjà un élément
        {
            GameObject gameObject = other.gameObject;
            if (gameObject.name.StartsWith("item") && gameObject.GetComponent<XRGrabInteractable>())
            {
                Debug.Log("Item dans inventaire !");
                AttachToCube(gameObject);
            }
        }
    }

    void AttachToCube(GameObject item)
    {
        // Stocke l'échelle d'origine de l'élément
        originalScale = item.transform.localScale;

        // Définir le cube (ce GameObject) comme parent de l'item
        item.transform.SetParent(this.transform);

        // Réinitialiser la position locale de l'item pour qu'il soit positionné correctement dans le cube
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f); // Réduit la taille de l'item

        var rigidbody = item.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        // Remet le grab
        var grabInteractable = item.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnItemGrabbed);
        }

        hasItem = true; // Marque le cube comme contenant un élément
    }

    void OnItemGrabbed(SelectExitEventArgs args)
    {
        GameObject item = args.interactableObject.transform.gameObject;

        // Détacher l'item du cube
        item.transform.SetParent(null);

        // Rendre le rigidbody non-cinématique pour permettre les interactions physiques
        var rigidbody = item.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }

        // Permettre à l'item d'être repris
        var grabInteractable = item.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnItemGrabbed); // Nettoyer l'événement pour éviter les appels multiples
        }

        // Réinitialiser l'échelle de l'item pour sa taille originale
        item.transform.localScale = originalScale;

        hasItem = false; // Marque le cube comme ne contenant plus d'élément
    }
}
