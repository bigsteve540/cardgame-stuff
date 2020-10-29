public struct Vector2
{
    public int x;
    public int y;

    public Vector2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator == (Vector2 _a, Vector2 _b)
    {
        return (_a.x == _b.x && _a.y == _b.y);
    }

    public static bool operator != (Vector2 _a, Vector2 _b)
    {
        return !(_a.x == _b.x && _a.y == _b.y);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

