using UnityEngine;

internal struct ShaderPair
{
	public readonly Shader Normal;

	public readonly Shader Alternate;

	public ShaderPair(Shader normalShader, Shader alternateShader)
	{
		Normal = normalShader;
		Alternate = alternateShader;
	}

	public Shader Get(bool usingAlternate)
	{
		return (!usingAlternate) ? Normal : Alternate;
	}
}
