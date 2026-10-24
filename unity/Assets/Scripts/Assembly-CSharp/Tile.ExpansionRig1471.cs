using UnityEngine;

public partial class Tile
{
	// Equivalent to the donor's additionDic/TilePool binding, without making
	// pooled IDs dependent on whichever world happens to be current on return.
	private void BindExpansionRig()
	{
		newAnimTrasFromAnimation = null;
		AnimParticleSystem = null;
		TileParticle = null;
		foreach (Transform child in AdditionTransform.GetComponentsInChildren<Transform>(true))
		{
			if (child.name == "part1") newAnimTrasFromAnimation = child.GetComponent<Animation>();
			if (child.name == "particles") AnimParticleSystem = child.GetComponent<ParticleSystem>();
			if (child.name == "Particle") TileParticle = child.GetComponent<ParticleSystem>();
		}
	}

	private bool CreateExpansionAnimator()
	{
		if (!ExpansionGameplay.Enabled || (int)animType < (int)AnimType.MoveJumpLeft) return false;
		AnimTransform = AltAnimTransform = null;
		AnimPartTransforms = null;
		if (animType == AnimType.MoveJumpLeft || animType == AnimType.MoveJumpRight || animType == AnimType.Rotation) return true;
		BindExpansionRig();
		switch (animType)
		{
		case AnimType.EnemyLaser:
			AdditionTransform.eulerAngles = Vector3.zero;
			AnimParticleSystem.gameObject.SetActive(false);
			break;
		case AnimType.EnemyRedLaserH:
		case AnimType.EnemyGreenLaserH:
			AdditionTransform.eulerAngles = Vector3.zero;
			TransformUtils.Find("anim", AdditionTransform).eulerAngles = new Vector3(0f, 0f, 90f);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			AnimParticleSystem.gameObject.SetActive(false);
			break;
		case AnimType.EnemyFloaterAirFallGround:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			break;
		case AnimType.EnemyLv9TakeBack:
			AdditionTransform.localPosition = new Vector3(0f, 0f, -1f);
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			break;
		case AnimType.RoadUp:
			AnimTransform = TransformUtils.FindAll("part", AdditionTransform)[0];
			break;
		case AnimType.EnemeyLaserBlock:
		case AnimType.EnemyVerticalCannon:
			AdditionTransform.eulerAngles = new Vector3(-90f, 0f, 0f);
			if (animType == AnimType.EnemyVerticalCannon) AdditionTransform.localPosition = Vector3.zero;
			break;
		case AnimType.EnemyRotateFourBlock:
		case AnimType.EnemyRetroSnaker01:
		case AnimType.EnemyRetroSnaker02:
		case AnimType.EnemySnakerWallLeft:
		case AnimType.EnemySnakerWallRight:
		case AnimType.EnemySnakerFrontMove:
		case AnimType.EnemySnakerCoiled01:
		case AnimType.EnemySnakerLeftMove:
		case AnimType.EnemySnakerRightMove:
			AdditionTransform.eulerAngles = new Vector3(90f, 180f, 0f);
			AdditionTransform.localPosition = Vector3.zero;
			if (animType == AnimType.EnemyRetroSnaker01) AdditionTransform.localPosition = new Vector3(0f, 0f, 0.005f);
			if (animType == AnimType.EnemyRetroSnaker02 || animType == AnimType.EnemySnakerWallLeft || animType == AnimType.EnemySnakerWallRight || animType == AnimType.EnemySnakerFrontMove || animType == AnimType.EnemySnakerCoiled01)
				AdditionTransform.localPosition = new Vector3(0f, 0f, 0.001f);
			break;
		case AnimType.EnemyPanda:
		case AnimType.BigBean:
			AdditionTransform.eulerAngles = new Vector3(90f, 180f, 0f);
			break;
		case AnimType.EnemyExtendTile:
			newAnimTrasFromAnimation.Play("0");
			extendPartices = TransformUtils.FindAll("tx_shensuo", AdditionTransform);
			int theme = LevelDesigner.GeneratingDonorThemeNumber;
			Color color = theme == 35 ? new Color(4f / 85f, 82f / 85f, 0f, 1f) :
				(theme == 36 ? new Color(40f / 51f, 40f / 51f, 40f / 51f, 40f / 51f) : new Color(0f, 54f / 85f, 1f, 1f));
			foreach (Transform particle in extendPartices)
			{
				particle.GetComponent<ParticleSystem>().startColor = color;
				particle.gameObject.SetActive(false);
			}
			break;
		case AnimType.EnemyDestroyUp:
		case AnimType.EnemyDestroyMove:
			GeoTransform.eulerAngles = Vector3.zero;
			BindTetrisAnimation(true);
			break;
		default:
			AdditionTransform.eulerAngles = Vector3.zero;
			break;
		}
		SetExpansionFootprint();
		return true;
	}

	private void SetAppointTileDanger(int y, int z)
	{
		MathUtils.IntTrio index = new MathUtils.IntTrio(Index.x, y, z);
		if (y >= 0 && z >= 0 && z <= 4 && GameManager.TileBoard[index] != null)
		{
			GameManager.TileBoard[index].dangerType = DangerType.Danger;
			GameManager.TileBoard[index].IsDangerous = true;
		}
	}

	private void SetExpansionFootprint()
	{
		int y = Index.y, x = Index.z;
		switch (animType)
		{
		case AnimType.EnemyRetroSnaker01: case AnimType.EnemyRetroSnaker02:
		case AnimType.EnemyChrisCard_Snowman: case AnimType.EnemyChrisCard_Reindeer: case AnimType.EnemyChrisCard_SantaClaus:
			SetAppointTileDanger(y, x - 1); SetAppointTileDanger(y, x + 1); break;
		case AnimType.EnemySnakerWallRight: SetAppointTileDanger(y, x - 1); break;
		case AnimType.EnemySnakerWallLeft: case AnimType.EnemyChrisCard_Environmental: SetAppointTileDanger(y, x + 1); break;
		case AnimType.EnemySnakerFrontMove:
			for (int n = 1; n < 5; n++) SetAppointTileDanger(y - n, x);
			break;
		case AnimType.EnemySnakerCoiled01:
			for (int n = -2; n < 3; n++) { SetAppointTileDanger(y + 3, x + n); SetAppointTileDanger(y - 1, x + n); }
			for (int n = -1; n < 2; n++) { SetAppointTileDanger(y + 1 + n, x + 2); SetAppointTileDanger(y + 1 + n, x - 2); }
			break;
		case AnimType.EnemyGhostFormation1:
			SetAppointTileDanger(y + 3, x); SetAppointTileDanger(y + 4, x - 1); SetAppointTileDanger(y + 4, x + 1); SetAppointTileDanger(y + 5, x); break;
		case AnimType.EnemyGhostFormation2:
			SetAppointTileDanger(y + 2, x); SetAppointTileDanger(y + 4, x - 1); SetAppointTileDanger(y + 4, x + 1); SetAppointTileDanger(y + 6, x); break;
		case AnimType.EnemyDoorOpen: case AnimType.EnemyDoorClose:
			SetAppointTileDanger(y, x - 2); SetAppointTileDanger(y, x + 2); SetAppointTileDanger(y + 1, x - 1); SetAppointTileDanger(y + 1, x + 1); break;
		case AnimType.EnemyChrisCard_Spirit:
			SetAppointTileDanger(y, x - 2); SetAppointTileDanger(y, x + 2); SetAppointTileDanger(y, x - 1); SetAppointTileDanger(y, x + 1); break;
		case AnimType.EnemyChrisCard_GingerbreadMan:
			SetAppointTileDanger(y, x - 2); SetAppointTileDanger(y, x - 1); SetAppointTileDanger(y, x + 1); break;
		case AnimType.RotateChristmasTree:
			SetAppointTileDanger(y, x - 1); SetAppointTileDanger(y, x + 1); SetAppointTileDanger(y + 1, x); SetAppointTileDanger(y - 1, x); break;
		}
	}
}
