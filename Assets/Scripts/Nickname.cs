using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Nickname : MonoBehaviour
{
    [SerializeField]
    Button NameButton;

    [SerializeField]
    Button SurnameButton;

    [SerializeField]
    Text Name;

    [SerializeField]
    Text Surname;

    List<string> names;
    List<string> surnames;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset namefile = (TextAsset)Resources.Load("UI/Names");
        TextAsset surnamefile = (TextAsset)Resources.Load("UI/Surnames");
        Debug.Log(namefile.text);
        names = namefile.text.Split("\n"[0]).ToList();
        Debug.Log(names.Count);
        surnames = surnamefile.text.Split("\n"[0]).ToList();
        NameButton.onClick.AddListener(() => GenerateName());
        SurnameButton.onClick.AddListener(() => GenerateSurname());
    }

    void GenerateName()
    {
        Name.text = names[Random.Range(0, names.Count)];
    }

    void GenerateSurname()
    {
        Surname.text = surnames[Random.Range(0, surnames.Count)];
    }
}
