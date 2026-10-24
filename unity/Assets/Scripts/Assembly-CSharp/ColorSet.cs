using UnityEngine;

internal class ColorSet
{
	public SmartColor Primary { get; private set; }

	public SmartColor Secondary { get; private set; }

	public SmartColor GuiLight { get; private set; }

	public SmartColor GuiLightAlt { get; private set; }

	public SmartColor GuiMid { get; private set; }

	public SmartColor GuiMidAlt { get; private set; }

	public SmartColor GuiDark { get; private set; }

	public SmartColor GuiButtonText { get; private set; }

	public SmartColor GuiReveal { get; private set; }

	public SmartColor GuiOverlay { get; private set; }

	public float GuiOverlayAlpha { get; private set; }

	public SmartColor BallDiff { get; private set; }

	public SmartColor BallSpec { get; private set; }

	public SmartColor BallSpecAmbi { get; private set; }

	public SmartColor EnemySpec { get; private set; }

	public float EnemySpecAmnt { get; private set; }

	public float EnemyGloss { get; private set; }

	public SmartColor PickupDiff { get; private set; }

	public SmartColor PickupSpec { get; private set; }

	public SmartColor PickupRefl { get; private set; }

	public SmartColor BackgroundFog { get; private set; }

	public SmartColor ForegroundFog { get; private set; }

	public SmartColor Camera { get; private set; }

	public SmartColor Score { get; private set; }

	public int FogStart { get; private set; }

	public int FogEnd { get; private set; }

	public SmartColor EnergyHigh { get; private set; }

	public SmartColor EnergyMidHigh { get; private set; }

	public SmartColor Energy { get; private set; }

	public SmartColor EnergyMidLow { get; private set; }

	public SmartColor EnergyLow { get; private set; }

	public Color[] EnergyColors { get; private set; }

	public Color[] EnergyParticles { get; private set; }

	public Color[] PickupParticles { get; private set; }

	public ColorSet(int primaryR, int primaryG, int primaryB, int secondaryR, int secondaryG, int secondaryB, int guiLightR, int guiLightG, int guiLightB, int guiLightAltR, int guiLightAltG, int guiLightAltB, int guiMidR, int guiMidG, int guiMidB, int guiMidAltR, int guiMidAltG, int guiMidAltB, int guiDarkR, int guiDarkG, int guiDarkB, int guiButtonTextR, int guiButtonTextG, int guiButtonTextB, int guiRevealR, int guiRevealG, int guiRevealB, int guiOverlayR, int guiOverlayG, int guiOverlayB, float guiOverlayAlpha, int energyHighR, int energyHighG, int energyHighB, int energyR, int energyG, int energyB, int energyLowR, int energyLowG, int energyLowB, int pickupDiffR, int pickupDiffG, int pickupDiffB, int pickupReflR, int pickupReflG, int pickupReflB, int pickupParticles0R, int pickupParticles0G, int pickupParticles0B, int pickupParticles1R, int pickupParticles1G, int pickupParticles1B, int pickupParticles2R, int pickupParticles2G, int pickupParticles2B, int pickupParticles3R, int pickupParticles3G, int pickupParticles3B, int pickupParticles4R, int pickupParticles4G, int pickupParticles4B, int ballDiffR, int ballDiffG, int ballDiffB, int ballSpecR, int ballSpecG, int ballSpecB, int ballSpecAmbiR, int ballSpecAmbiG, int ballSpecAmbiB, int backgroundFogR, int backgroundFogG, int backgroundFogB, int foregroundFogR, int foregroundFogG, int foregroundFogB, int enemySpecR, int enemySpecG, int enemySpecB, float enemySpecular, float enemyGloss, int fogStart, int fogEnd)
	{
		Primary = new SmartColor(primaryR, primaryG, primaryB);
		Secondary = new SmartColor(secondaryR, secondaryG, secondaryB);
		GuiLight = new SmartColor(guiLightR, guiLightG, guiLightB);
		GuiLightAlt = new SmartColor(guiLightAltR, guiLightAltG, guiLightAltB);
		GuiMid = new SmartColor(guiMidR, guiMidG, guiMidB);
		GuiMidAlt = new SmartColor(guiMidAltR, guiMidAltG, guiMidAltB);
		GuiDark = new SmartColor(guiDarkR, guiDarkG, guiDarkB);
		GuiButtonText = new SmartColor(guiButtonTextR, guiButtonTextG, guiButtonTextB);
		GuiReveal = new SmartColor(guiRevealR, guiRevealG, guiRevealB);
		GuiOverlay = new SmartColor(guiOverlayR, guiOverlayG, guiOverlayB);
		EnergyHigh = new SmartColor(energyHighR, energyHighG, energyHighB);
		Energy = new SmartColor(energyR, energyG, energyB);
		EnergyLow = new SmartColor(energyLowR, energyLowG, energyLowB);
		PickupDiff = new SmartColor(pickupDiffR, pickupDiffG, pickupDiffB);
		PickupRefl = new SmartColor(pickupReflR, pickupReflG, pickupReflB);
		BallDiff = new SmartColor(ballDiffR, ballDiffG, ballDiffB);
		BallSpec = new SmartColor(ballSpecR, ballSpecG, ballSpecB);
		BallSpecAmbi = new SmartColor(ballSpecAmbiR, ballSpecAmbiG, ballSpecAmbiB);
		BackgroundFog = new SmartColor(backgroundFogR, backgroundFogG, backgroundFogB);
		ForegroundFog = new SmartColor(foregroundFogR, foregroundFogG, foregroundFogB);
		EnemySpec = new SmartColor(enemySpecR, enemySpecG, enemySpecB);
		EnemySpecAmnt = enemySpecular;
		EnemyGloss = enemyGloss;
		GuiOverlayAlpha = guiOverlayAlpha;
		Camera = BackgroundFog;
		FogStart = fogStart;
		FogEnd = fogEnd;
		Score = ForegroundFog.GetBlendedCopy(Color.white, 0.65f);
		PickupSpec = BallSpec.GetCopy();
		EnergyMidHigh = Energy.GetBlendedCopy(EnergyHigh, 0.5f);
		EnergyMidLow = Energy.GetBlendedCopy(EnergyLow, 0.5f);
		EnergyColors = new Color[5] { EnergyHigh.color, EnergyMidHigh.color, Energy.color, EnergyMidLow.color, EnergyLow.color };
		EnergyParticles = EnergyColors;
		EnergyParticles[0].a = 0f;
		EnergyParticles[1].a = 1f;
		EnergyParticles[2].a = 0.6666f;
		EnergyParticles[3].a = 0.3333f;
		EnergyParticles[4].a = 0f;
		SmartColor smartColor = new SmartColor(pickupParticles0R, pickupParticles0G, pickupParticles0B, 0);
		SmartColor smartColor2 = new SmartColor(pickupParticles1R, pickupParticles1G, pickupParticles1B, 128);
		SmartColor smartColor3 = new SmartColor(pickupParticles2R, pickupParticles2G, pickupParticles2B, 128);
		SmartColor smartColor4 = new SmartColor(pickupParticles3R, pickupParticles3G, pickupParticles3B, 128);
		SmartColor smartColor5 = new SmartColor(pickupParticles4R, pickupParticles4G, pickupParticles4B, 0);
		PickupParticles = new Color[5] { smartColor.color, smartColor2.color, smartColor3.color, smartColor4.color, smartColor5.color };
	}
}
