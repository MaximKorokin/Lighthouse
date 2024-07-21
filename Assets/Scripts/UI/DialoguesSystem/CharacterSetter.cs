using UnityEngine;

public class CharacterSetter : MonoBehaviour
{
    [field: SerializeField]
    [field: DataMapping(typeof(CharactersPreviewsDataBase))]
    public string CharacterPreviewId { get; private set; }

    private void Awake()
    {
        CharacterMapper.SetCharacter(CharacterPreviewId, gameObject);
    }

    private void OnDestroy()
    {
        CharacterMapper.RemoveCharacter(CharacterPreviewId, gameObject);
    }
}
