using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Nickname : MonoBehaviour
{
    List<string> names;
    List<string> nicknames;

    void Start()
    {
        TextAsset namefile = (TextAsset)Resources.Load("UI/Names");
        TextAsset nicknamefile = (TextAsset)Resources.Load("UI/Surnames");
        names = namefile.text.Split("\n"[0]).ToList();
        nicknames = nicknamefile.text.Split("\n"[0]).ToList();
    }

    public string GenerateName()
    {
        return names[Random.Range(0, names.Count)] + " " + nicknames[Random.Range(0, nicknames.Count)];
    }
}
