using UnityEngine;

public class CharacterSetter : MonoBehaviour
{
    [field: SerializeField]
    [field: DataMapping(typeof(CharactersPreviewsDataBase))]
    public string CharacterPreviewId { get; private set; }

    private void Awake()
    {
        CharactersMapper.SetCharacter(CharacterPreviewId, gameObject);
    }

    private void OnDestroy()
    {
        CharactersMapper.RemoveCharacter(CharacterPreviewId, gameObject);
    }
}
