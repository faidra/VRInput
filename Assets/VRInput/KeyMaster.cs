
public class KeySet
{
    public bool IsTapTrigger { get; set; }
    public string Tap { get; set; }
    public string Snap { get; set; }
    public string Left { get; set; }
    public string Up { get; set; }
    public string Right { get; set; }
    public string Down { get; set; }
}

public class KeyMap
{
    public KeySet Center { get; set; }
    public KeySet Left { get; set; }
    public KeySet Up { get; set; }
    public KeySet Right { get; set; }
    public KeySet Down { get; set; }
}

public class KeyMaster
{
    public KeyMap Off { get; set; }
    public KeyMap Center { get; set; }
    public KeyMap Left { get; set; }
    public KeyMap Up { get; set; }
    public KeyMap Right { get; set; }
    public KeyMap Down { get; set; }
}
