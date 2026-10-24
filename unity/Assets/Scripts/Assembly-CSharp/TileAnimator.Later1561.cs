using UnityEngine;

public partial class TileAnimator
{
	private static bool AnimateLaterTile(int row, int col)
	{
		if (!ExpansionGameplay.Later) return false;
		switch (animatedRows[row][col].animType)
		{
		case Tile.AnimType.EnemyCube1: case Tile.AnimType.EnemyCube3: case Tile.AnimType.EnemyCube3Pillar:
		case Tile.AnimType.EnemyCube4: case Tile.AnimType.EnemyCube6: case Tile.AnimType.EnemyCube9: case Tile.AnimType.EnemyHalfround:
			ExpansionClip(row, col, 20f, -3f, 9f, -6f, "anim1", false); break;
		case Tile.AnimType.EnemyCube5: ExpansionClip(row, col, 20f, -3f, 13f, -10f, "anim1", false); break;
		case Tile.AnimType.TurnTileCenter: ExpansionClip(row, col, 18f, -3f, 4f, -10f, "center", false); break;
		case Tile.AnimType.TurnTileLeft1: ExpansionClip(row, col, 18f, -3f, 4f, -10f, "left1", false); break;
		case Tile.AnimType.TurnTileLeft2: ExpansionClip(row, col, 18f, -3f, 4f, -10f, "left2", false); break;
		case Tile.AnimType.TurnTileRight1: ExpansionClip(row, col, 18f, -3f, 4f, -10f, "right1", false); break;
		case Tile.AnimType.TurnTileRight2: ExpansionClip(row, col, 18f, -3f, 4f, -10f, "right2", false); break;
		case Tile.AnimType.EnemyLv20Roller: ExpansionClip(row, col, 20f, -3f, 9.5f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyPoker1: ExpansionClip(row, col, 20f, -3f, 15f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyPoker2Left: case Tile.AnimType.EnemyPoker2Right: case Tile.AnimType.EnemyPoker3Left: case Tile.AnimType.EnemyPoker3Right:
		case Tile.AnimType.EnemyBigJoker: case Tile.AnimType.EnemySmallJoker: case Tile.AnimType.EnemyLv25DownLightning:
			ExpansionClip(row, col, 15f, -3f, 10f, -20f, "anim1", false); break;
		case Tile.AnimType.EnemyMagicDove: ExpansionClip(row, col, 20f, -3f, 13f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyPoker1Lean: ExpansionClip(row, col, 15f, -3f, 10f, -10f, "anim1", false); break;
		case Tile.AnimType.EnemyPokerTree: ExpansionClip(row, col, 20f, -3f, 10f, -20f, "anim1", false); break;
		case Tile.AnimType.EnemyPokerTreeSlider: ExpansionClip(row, col, 20f, -3f, 10f, -20f, "anim1", false); SlideSlider(row, col); break;
		case Tile.AnimType.EnemyTileWhite: case Tile.AnimType.EnemyTileBlack: ExpansionClip(row, col, 18f, -3f, 5f, -11f, "anim1", false); break;
		case Tile.AnimType.EnemyUmbrellaLeft: case Tile.AnimType.EnemyUmbrellaRight: case Tile.AnimType.EnemySoldierLeft: case Tile.AnimType.EnemySoldierRight:
		case Tile.AnimType.EnemyLv25SoundLeft: case Tile.AnimType.EnemyLv25SoundRight:
			ExpansionClip(row, col, 18f, -3f, 13.5f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyPokerDoor: ExpansionClip(row, col, 15f, -3f, 13f, -20f, "anim1", false); break;
		case Tile.AnimType.EnemyLevel23TileDown: ExpansionClip(row, col, 20f, -3f, 11f, -13f, "anim1", false); break;
		case Tile.AnimType.PickDownLv25Cube: case Tile.AnimType.PickDownLv25Cube1: ExpansionClip(row, col, 18f, -3f, 11f, -20f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25Sound: ExpansionClip(row, col, 18f, -3f, 11f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25Brigde: case Tile.AnimType.BeanLv25BrigdeUp: case Tile.AnimType.BeanLv25BrigdeDown: case Tile.AnimType.EnemyLv25AirEye:
			ExpansionClip(row, col, 18f, -3f, 10f, -11f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25XiaoBianLeft: case Tile.AnimType.EnemyLv25XiaoBianRight: ExpansionClip(row, col, 18f, -3f, 15f, -30f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25Tape: case Tile.AnimType.EnemyLv25Sound3: case Tile.AnimType.EnemyLv25CocountTree:
			ExpansionClip(row, col, 18f, -3f, 10f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25CubeLive: case Tile.AnimType.EnemyLv25CubeDie: ExpansionClip(row, col, 18f, 0f, 10f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25MoveDici: ExpansionClip(row, col, 18f, -3f, 13.5f, -20f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25Spring: ExpansionClip(row, col, 18f, -3f, 7f, -15f, "anim1", false); break;
		case Tile.AnimType.EnemyLv25Gyroscope:
			Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
			float distance = animatedRows[row].Distance;
			if (distance < 18.2f && distance > -5f && tile.AnimTransform != null)
				tile.AnimTransform.Rotate(Vector3.up * (distance <= 0f ? 720f : 180f) * Time.deltaTime * (tile.direction == Direction.Left ? 1 : -1));
			break;
		default: return false;
		}
		return true;
	}
}
