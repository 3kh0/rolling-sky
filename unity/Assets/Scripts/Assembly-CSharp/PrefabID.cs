public struct PrefabID
{
	public readonly PrefabType prefabType;

	public readonly PrefabName prefabName;

	public readonly bool IsNull;

	public bool IsNotNull
	{
		get
		{
			return !IsNull;
		}
	}

	public static PrefabID Null
	{
		get
		{
			return new PrefabID(PrefabType.Null, PrefabName.Null, true);
		}
	}

	public PrefabID(PrefabType prefabType, PrefabName prefabName)
	{
		this.prefabType = prefabType;
		this.prefabName = prefabName;
		IsNull = false;
	}

	private PrefabID(PrefabType prefabType, PrefabName prefabName, bool isNull)
	{
		this.prefabType = prefabType;
		this.prefabName = prefabName;
		IsNull = isNull;
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "Null";
		}
		return prefabType.ToString() + "." + prefabName;
	}
}
