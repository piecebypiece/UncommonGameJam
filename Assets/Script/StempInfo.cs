using System;

[Serializable]
public struct StempInfo
{
    public enum Kind
    {
        Emoji,
        Word,
    }

    public Kind kind;
    public string key;
    public string UserID;
}