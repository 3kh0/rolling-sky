using UnityEngine;

public class BlockManager : MonoBehaviour
{
	private class GravityForce
	{
		private float value;

		private float delay;

		private bool isDelayed;

		public bool StillInUse { get; private set; }

		public bool IsActive { get; private set; }

		public GravityForce()
		{
			IsActive = true;
		}

		public GravityForce(float delay)
		{
			this.delay = delay;
			isDelayed = true;
		}

		public void UpdateForce()
		{
			if (isDelayed)
			{
				delay -= Time.smoothDeltaTime;
				if (delay <= 0f)
				{
					value = (0f - delay) * 0.1f;
					isDelayed = false;
					IsActive = true;
				}
			}
			else
			{
				value += gravityForce;
			}
			StillInUse = false;
		}

		public float Get()
		{
			StillInUse = true;
			return value;
		}

		public float Get(out bool active)
		{
			StillInUse = true;
			active = IsActive;
			return value;
		}
	}

	private class GeoBuffer
	{
		private ArrayUtils.List<Transform> buffer;

		private ArrayUtils.List<Material> materials;

		private Transform master;

		private Material sharedMaterial;

		public int Size { get; private set; }

		public bool HasMaterials { get; private set; }

		public int Active { get; private set; }

		public int Created { get; private set; }

		public int Available
		{
			get
			{
				return Created - Active;
			}
		}

		public GeoBuffer(Transform masterTransform, int bufferSize)
		{
			ConfigureGeoBuffer(masterTransform, bufferSize, null);
		}

		public GeoBuffer(Transform masterTransform, int bufferSize, params Material[] bufferMaterials)
		{
			ConfigureGeoBuffer(masterTransform, bufferSize, bufferMaterials);
		}

		private void ConfigureGeoBuffer(Transform masterTransform, int bufferSize, Material[] bufferMaterials)
		{
			master = masterTransform;
			Object.DontDestroyOnLoad(master);
			TransformUtils.Hide(master);
			master.parent = bufferedGroup;
			Size = bufferSize;
			Active = 0;
			Created = 0;
			buffer = new ArrayUtils.List<Transform>(2000);
			for (int i = 0; i < Size; i++)
			{
				buffer.Add(Create());
			}
			HasMaterials = bufferMaterials != null;
			materials = new ArrayUtils.List<Material>(bufferMaterials);
			sharedMaterial = masterTransform.GetComponent<Renderer>().sharedMaterial;
		}

		public Transform Get()
		{
			return Get(0);
		}

		public Transform Get(int materialIndex)
		{
			Transform transform;
			if (buffer.IsEmpty)
			{
				Debug.Log("BKMN: WARNING: Needed to create new voxel because ran out of buffered voxels");
				transform = TransformUtils.Duplicate(master);
			}
			else
			{
				transform = buffer.Last;
				buffer.RemoveLast();
			}
			Active++;
			TryAssignMaterial(transform, materialIndex);
			return transform;
		}

		public void Give(Transform voxel)
		{
			voxel.GetComponent<Renderer>().material = sharedMaterial;
			TransformUtils.Hide(voxel);
			buffer.Add(voxel);
			Active--;
		}

		private Transform Create()
		{
			Transform transform = TransformUtils.Duplicate(master);
			Object.DontDestroyOnLoad(transform);
			transform.parent = bufferedGroup;
			Created++;
			return transform;
		}

		private void TryAssignMaterial(Transform geoTransform, int materialIndex)
		{
			if (HasMaterials)
			{
				geoTransform.GetComponent<Renderer>().material = materials[materialIndex];
			}
		}
	}

	public class Building
	{
		private ArrayUtils.Array<Block> blocks;

		private Block singleBlock;

		private bool singleBlockBuilding;

		private Material material;

		public Vector3 Size { get; private set; }

		public Vector3 Center { get; private set; }

		public Vector3 Corner { get; private set; }

		public Vector3 CornerTop { get; private set; }

		public bool IsActive { get; private set; }

		public Building(params Transform[] buildingBlocks)
		{
			if (buildingBlocks.Length == 0)
			{
				Debug.LogError("BKMN.BLDG: ERROR: Attempt to create a building from 0 blocks - please parse atleast ONE transform to Building class upon creation");
			}
			else if (buildingBlocks.Length == 1)
			{
				singleBlockBuilding = true;
				singleBlock = new Block(buildingBlocks[0]);
				Center = singleBlock.Center;
				Size = singleBlock.Size;
				Corner = singleBlock.Corner;
				CornerTop = singleBlock.CornerTop;
			}
			else
			{
				Debug.LogError("BKMN.BLDG: ERROR: Attempt to create a building from multiple blocks - multi-block buldings not yet configured");
			}
			Reset();
		}

		public void Reset()
		{
			IsActive = false;
		}

		public void Clear()
		{
			blocks.Clear();
			singleBlockBuilding = false;
			singleBlock = null;
			material = null;
			Size = Vector3.zero;
		}

		public bool Update()
		{
			bool isActive = false;
			if (singleBlockBuilding)
			{
				isActive = singleBlock.Update();
			}
			else
			{
				for (int i = 0; i < blocks.Length; i++)
				{
					if (blocks[i].IsActive && blocks[i].Update())
					{
						isActive = true;
					}
				}
			}
			IsActive = isActive;
			return IsActive;
		}

		public void Impact(Vector3 impactPoint, float impactForce, float destructiveForce)
		{
			bool flag = false;
			if (singleBlockBuilding)
			{
				singleBlock.Impact(impactPoint, impactForce, destructiveForce);
				flag = true;
			}
			else
			{
				for (int i = 0; i < blocks.Length; i++)
				{
					if (blocks[i].Contains(impactPoint, impactForce))
					{
						blocks[i].Impact(impactPoint, impactForce, destructiveForce);
						flag = true;
					}
				}
				Debug.LogError("BKMN.BLDG: ERROR: Attempt to RandomImpact() a building from multiple blocks - multi-block buldings not yet configured");
			}
			if (flag)
			{
				IsActive = true;
			}
		}

		public void RandomImpact()
		{
			if (singleBlockBuilding)
			{
				singleBlock.RandomImpact();
			}
			else
			{
				Debug.LogError("BKMN.BLDG: ERROR: Attempt to RandomImpact() a building from multiple blocks - multi-block buldings not yet configured");
			}
			IsActive = true;
		}

		public bool Contains(Vector3 point, float margin)
		{
			return point.x + margin >= Corner.x && point.x - margin < CornerTop.x && point.y + margin >= Corner.y && point.y - margin < CornerTop.y && point.z + margin >= Corner.z && point.z - margin < CornerTop.z;
		}

		public bool CanDetonate(ref Vector3 point, Vector3 direction)
		{
			bool flag;
			if (Contains(point, 0.1f))
			{
				if (singleBlockBuilding)
				{
					Vector3 detonationPoint;
					flag = singleBlock.CanDetonate(point, direction, out detonationPoint);
					if (flag)
					{
						point = detonationPoint;
					}
				}
				else
				{
					Debug.LogError("BKMN.BLDG: ERROR: Attempt to check Contains() on a building with multiple blocks - multi-block buldings not yet configured");
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool HasTransform(Transform transform)
		{
			if (singleBlockBuilding)
			{
				return singleBlock.Transform.Equals(transform);
			}
			Debug.LogError("BKMN.BLDG: ERROR: Attempt to check HasTransform() on a building with multiple blocks - multi-block buldings not yet configured");
			return false;
		}
	}

	public class Block
	{
		private BoxCollider collider;

		private ArrayUtils.TrinitiedMultiArray<Voxel> voxels;

		private ArrayUtils.MultiArray<MathUtils.Tri> structural;

		private ArrayUtils.List<GravityForce> gravityForces;

		private float xOffset;

		private float zOffset;

		public Transform Transform { get; private set; }

		public Vector3 Center { get; private set; }

		public Vector3 Corner { get; private set; }

		public Vector3 CornerTop { get; private set; }

		public Vector3 Size { get; private set; }

		public MathUtils.IntTrio SizeInVoxels { get; private set; }

		public MathUtils.IntTrio TopVoxels { get; private set; }

		public bool IsBroken { get; private set; }

		public bool IsActive { get; private set; }

		public bool IsNotBroken
		{
			get
			{
				return !IsBroken;
			}
		}

		public bool IsNotActive
		{
			get
			{
				return !IsActive;
			}
		}

		public Block(Transform blockTransform)
		{
			Transform = blockTransform;
			collider = Transform.GetComponent<BoxCollider>();
			if (collider == null)
			{
				collider = Transform.gameObject.AddComponent<BoxCollider>();
			}
			Center = Transform.position;
			Size = collider.size;
			xOffset = ((!(Center.x < 0f)) ? 0f : 0.05f);
			zOffset = ((!(Center.z < 0f)) ? 0f : 0.05f);
			SizeInVoxels = new MathUtils.IntTrio(MathUtils.FlooredDivision(Size.x + 0.025f, 0.05f), MathUtils.FlooredDivision(Size.y + 0.025f, 0.05f), MathUtils.FlooredDivision(Size.z + 0.025f, 0.05f));
			TopVoxels = new MathUtils.IntTrio(SizeInVoxels.x - 1, SizeInVoxels.y - 1, SizeInVoxels.z - 1);
			Corner = new Vector3(Center.x - Size.x / 2f + 0.025f, Center.y + 0.025f, Center.z - Size.z / 2f + 0.025f);
			CornerTop = Corner + Size;
			Reset();
		}

		public void Reset()
		{
			if (IsBroken)
			{
				Transform.GetComponent<Renderer>().enabled = true;
				for (int i = 0; i < voxels.Length; i++)
				{
					TryDestroyVoxel(i);
				}
				voxels.Clear();
				gravityForces.Clear();
			}
			IsBroken = false;
			IsActive = false;
		}

		public bool Update()
		{
			bool flag = false;
			for (int i = 0; i < gravityForces.Length; i++)
			{
				gravityForces[i].UpdateForce();
			}
			for (int j = 0; j < voxels.Length; j++)
			{
				if (voxels.IsNormalTrue(j) && voxels[new int[1] { j }].IsActive)
				{
					bool active;
					float voxelGravity = gravityForces[voxels[new int[1] { j }].GravityIndex].Get(out active);
					if (voxels[new int[1] { j }].Update(voxelGravity, active))
					{
						flag = true;
					}
					else
					{
						voxels.FlagSpecialTrue(j);
					}
				}
			}
			if (flag)
			{
				IsActive = true;
			}
			else
			{
				gravityForces.Reset();
				IsActive = false;
			}
			return IsActive;
		}

		public bool Contains(Vector3 point, float margin)
		{
			return point.x + margin >= Corner.x && point.x - margin < CornerTop.x && point.y + margin >= Corner.y && point.y - margin < CornerTop.y && point.z + margin >= Corner.z && point.z - margin < CornerTop.z;
		}

		public bool CanDetonate(Vector3 point, Vector3 direction, out Vector3 detonationPoint)
		{
			bool flag;
			if (Contains(point, 0.1f))
			{
				if (IsBroken)
				{
					direction *= 0.025f / direction.magnitude;
					flag = false;
					detonationPoint = Vector3.zero;
					bool flag2 = true;
					int num = 0;
					int num2 = 0;
					Color color = Color.black;
					do
					{
						MathUtils.IntTrio index = ToIndex(point);
						Color side;
						if (IsValid(index, out side))
						{
							if (voxels.IsNormal(index.x, index.y, index.z))
							{
								detonationPoint = point;
								flag = true;
								flag2 = false;
							}
						}
						else
						{
							num2++;
							if (num2 == 1)
							{
								color = side;
							}
							else if (num2 > 10)
							{
								flag2 = false;
							}
							else if (side != color)
							{
								flag2 = false;
								Clamp(ref index);
								if (voxels.IsNormal(index.x, index.y, index.z))
								{
									detonationPoint = point;
									flag = true;
									Debug.Log(string.Format("CHNK: Last second detonation against {0} of {1}", index, SizeInVoxels));
								}
								else
								{
									Debug.Log(string.Format("CHNK: Failed to last second detonate against {0} of {1}", index, SizeInVoxels));
								}
							}
						}
						if (flag)
						{
							side = Color.white;
						}
						Transform transform = null;
						if (flag2)
						{
							point += direction;
							num++;
						}
					}
					while (flag2);
				}
				else
				{
					flag = true;
					detonationPoint = point;
				}
			}
			else
			{
				flag = false;
				detonationPoint = Vector3.zero;
			}
			return flag;
		}

		public void RandomImpact()
		{
			Vector3 randomVector = Randomizer.GetRandomVector3(Corner, CornerTop);
			Impact(randomVector, 0.25f, 0.5f);
		}

		public void Impact(Vector3 impactPoint, float impactForce, float destructiveForce)
		{
			bool flag = TrySetupBreak();
			float num = impactForce + 0.05f;
			gravityForces.Add(new GravityForce());
			int lastIndex = gravityForces.LastIndex;
			for (int i = 0; i < SizeInVoxels.x; i++)
			{
				float xFloat = i;
				for (int j = 0; j < SizeInVoxels.y; j++)
				{
					float yFloat = j;
					for (int k = 0; k < SizeInVoxels.z; k++)
					{
						float zFloat = k;
						if (voxels.IsNotSpecialTrue(i, j, k))
						{
							bool flag2 = flag && (i == 0 || i == TopVoxels.x || j == TopVoxels.y || k == 0 || k == TopVoxels.z);
							Vector3 vector = ToPosition(xFloat, yFloat, zFloat);
							float num2 = MathUtils.Distance(vector, impactPoint);
							bool flag3 = num2 <= num;
							bool flag4 = num2 <= impactForce;
							bool destroyed = flag4 && Randomizer.RollAgainst(destructiveForce);
							Vector3 force;
							if (flag4)
							{
								force = new Vector3(vector.x - impactPoint.x, vector.y - impactPoint.y, vector.z - impactPoint.z) * 0.2f;
								IsActive = true;
							}
							else
							{
								force = Vector3.zero;
							}
							if (flag2 || flag3)
							{
								CreateVoxel(i, j, k, vector, force, flag3, flag4, destroyed, lastIndex, true);
							}
						}
					}
				}
			}
			TryTopple();
			FillHoles();
		}

		private bool TrySetupBreak()
		{
			bool isNotBroken = IsNotBroken;
			if (isNotBroken)
			{
				voxels = new ArrayUtils.TrinitiedMultiArray<Voxel>(SizeInVoxels);
				gravityForces = new ArrayUtils.List<GravityForce>();
				Transform.GetComponent<Renderer>().enabled = false;
				IsBroken = true;
			}
			return isNotBroken;
		}

		private void CreateVoxel(MathUtils.IntTrio voxelIndices, bool affected, bool active, Color? debugColor = null)
		{
			CreateVoxel(voxelIndices.x, voxelIndices.y, voxelIndices.z, affected, active, debugColor);
		}

		private void CreateVoxel(int x, int y, int z, bool affected, bool active, Color? debugColor = null)
		{
			CreateVoxel(x, y, z, ToPosition(x, y, z), Vector3.zero, affected, active, debugColor);
		}

		private void CreateVoxel(int x, int y, int z, Vector3 position, Vector3 force, bool affected, bool active, Color? debugColor = null)
		{
			CreateVoxel(x, y, z, position, force, affected, active, false, gravityForces.LastIndex, false, debugColor);
		}

		private void CreateVoxel(int x, int y, int z, Vector3 position, Vector3 force, bool affected, bool active, bool destroyed, int gravityIndex, bool allowFall, Color? debugColor = null)
		{
			if (!voxels.IsNotSpecialTrue(x, y, z))
			{
				return;
			}
			if (allowFall && voxels.IsTrue(x, y, z))
			{
				if (voxels[new int[3] { x, y, z }].IsAffected && voxels[new int[3] { x, y, z }].IsAttached)
				{
					active = true;
				}
				voxels[new int[3] { x, y, z }].Destroy();
			}
			voxels[new int[3] { x, y, z }] = new Voxel(position, force, affected, active, destroyed, gravityIndex, debugColor);
			bool trueOrFalse;
			bool specialTrueOrNot;
			if (destroyed)
			{
				trueOrFalse = true;
				specialTrueOrNot = true;
			}
			else
			{
				trueOrFalse = true;
				specialTrueOrNot = false;
			}
			voxels.FlagAs(trueOrFalse, specialTrueOrNot, x, y, z);
		}

		private void TryDestroyVoxel(MathUtils.IntTrio voxelIndices)
		{
			if (voxels.IsNormalTrue(voxelIndices))
			{
				voxels[voxelIndices].Destroy();
				voxels.FlagSpecialTrue(voxelIndices);
			}
		}

		private void TryDestroyVoxel(int voxelIndex)
		{
			if (voxels.IsNormalTrue(voxelIndex))
			{
				voxels[new int[1] { voxelIndex }].Destroy();
				voxels.FlagSpecialTrue(voxelIndex);
			}
		}

		private Vector3 ToPosition(float xFloat, float yFloat, float zFloat)
		{
			return new Vector3(Corner.x + xFloat * 0.05f + xOffset, Corner.y + yFloat * 0.05f, Corner.z + zFloat * 0.05f + zOffset);
		}

		private void CenterPosition(ref Vector3 voxelPosition)
		{
			voxelPosition.x += 0.025f;
			voxelPosition.y += 0.025f;
			voxelPosition.z += 0.025f;
		}

		private MathUtils.IntTrio ToIndex(Vector3 position)
		{
			position -= Corner;
			position /= 0.05f;
			return new MathUtils.IntTrio(position);
		}

		private void Clamp(ref MathUtils.IntTrio index)
		{
			if (index.x < 0)
			{
				index.x = 0;
			}
			else if (index.x >= SizeInVoxels.x)
			{
				index.x = SizeInVoxels.x - 1;
			}
			if (index.y < 0)
			{
				index.y = 0;
			}
			else if (index.y >= SizeInVoxels.y)
			{
				index.y = SizeInVoxels.y - 1;
			}
			if (index.z < 0)
			{
				index.z = 0;
			}
			else if (index.z >= SizeInVoxels.z)
			{
				index.z = SizeInVoxels.z - 1;
			}
		}

		private bool IsValid(MathUtils.IntTrio index)
		{
			return index.x >= 0 && index.x < SizeInVoxels.x && index.y >= 0 && index.y < SizeInVoxels.y && index.z >= 0 && index.z < SizeInVoxels.z;
		}

		private bool IsValid(MathUtils.IntTrio index, out Color side)
		{
			bool result = true;
			side = Color.grey;
			if (index.x < 0)
			{
				result = false;
				side.r = 0f;
			}
			else if (index.x >= SizeInVoxels.x)
			{
				result = false;
				side.r = 1f;
			}
			if (index.y < 0)
			{
				result = false;
				side.g = 0f;
			}
			else if (index.y >= SizeInVoxels.y)
			{
				result = false;
				side.g = 1f;
			}
			if (index.z < 0)
			{
				result = false;
				side.b = 0f;
			}
			else if (index.z >= SizeInVoxels.z)
			{
				result = false;
				side.b = 1f;
			}
			return result;
		}

		private void FillHoles()
		{
			for (int i = 0; i < SizeInVoxels.x; i++)
			{
				for (int j = 0; j < SizeInVoxels.y; j++)
				{
					for (int k = 0; k < SizeInVoxels.z; k++)
					{
						if (!voxels.IsFalse(i, j, k))
						{
							continue;
						}
						MathUtils.IntTrio intTrio = new MathUtils.IntTrio(0, 0, 0);
						for (int l = -1; l <= 1; l++)
						{
							for (int m = -1; m <= 1; m++)
							{
								for (int n = -1; n <= 1; n++)
								{
									int num = 0;
									if (l == 0)
									{
										num++;
									}
									if (m == 0)
									{
										num++;
									}
									if (n == 0)
									{
										num++;
									}
									if (num != 1 && num != 2)
									{
										continue;
									}
									intTrio.x = i + l;
									intTrio.y = j + m;
									intTrio.z = k + n;
									if (!IsValid(intTrio) || (!voxels[intTrio].IsActive && !voxels.IsSpecialTrue(intTrio)))
									{
										continue;
									}
									goto IL_00eb;
								}
							}
							continue;
							IL_00eb:
							CreateVoxel(i, j, k, false, false, Color.yellow);
							break;
						}
					}
				}
			}
		}

		private void TryTopple()
		{
			if (structural == null)
			{
				structural = new ArrayUtils.MultiArray<MathUtils.Tri>(SizeInVoxels);
			}
			else
			{
				structural.Reset();
			}
			for (int i = 0; i < SizeInVoxels.x; i++)
			{
				for (int j = 0; j < SizeInVoxels.z; j++)
				{
					if (structural[new int[3] { i, 0, j }].IsFalse && voxels.IsNotSpecialTrue(i, 0, j))
					{
						bool fullStability = voxels.IsFalse(i, 0, j) || voxels[new int[3] { i, 0, j }].IsUnaffected;
						TrySpreadStability(new MathUtils.IntTrio(i, 0, j), fullStability);
					}
				}
			}
			float num = 0f;
			for (int k = 0; k < SizeInVoxels.y; k++)
			{
				bool flag = true;
				for (int l = 0; l < SizeInVoxels.x; l++)
				{
					for (int m = 0; m < SizeInVoxels.z; m++)
					{
						if (structural[new int[3] { l, k, m }].IsFalse)
						{
							if (voxels.IsNormalTrue(l, k, m))
							{
								if (voxels[new int[3] { l, k, m }].IsAttached)
								{
									if (flag)
									{
										num += 0.04f;
										gravityForces.Add(new GravityForce(num));
										flag = false;
									}
									voxels[new int[3] { l, k, m }].Destroy();
									Vector3 position = ToPosition(l, k, m);
									Vector3 force = new Vector3(position.x - Center.x, 0.1f, position.z - Center.z) * 0.0075f;
									force *= Randomizer.GetRandomWeight(0.5f);
									CreateVoxel(l, k, m, position, force, true, true, Color.black);
								}
							}
							else if (voxels.IsFalse(l, k, m))
							{
								voxels.FlagSpecialTrue(l, k, m);
							}
						}
						else if (structural[new int[3] { l, k, m }].IsNormalTrue && voxels[new int[3] { l, k, m }].IsAttached)
						{
							voxels[new int[3] { l, k, m }].Color(Color.gray);
						}
					}
				}
			}
		}

		private void TrySpreadStability(MathUtils.IntTrio voxelIndex, bool fullStability)
		{
			for (int i = 0; i < 6; i++)
			{
				MathUtils.IntTrio intTrio = voxelIndex;
				switch (i)
				{
				case 0:
					intTrio.x--;
					break;
				case 1:
					intTrio.x++;
					break;
				case 2:
					intTrio.y--;
					break;
				case 3:
					intTrio.y++;
					break;
				case 4:
					intTrio.z--;
					break;
				case 5:
					intTrio.z++;
					break;
				default:
					Debug.LogError(string.Format("BKMN: ERROR: Attempt to spread stability to unhandled side number {0}", i));
					break;
				}
				if (!IsValid(intTrio) || !structural[intTrio].IsFalse)
				{
					continue;
				}
				bool flag = voxels.IsFalse(intTrio) || voxels[intTrio].IsUnaffected;
				bool flag2;
				bool flag3;
				if (fullStability)
				{
					if (flag)
					{
						flag2 = true;
						flag3 = true;
					}
					else
					{
						flag2 = false;
						flag3 = true;
					}
				}
				else
				{
					flag2 = false;
					flag3 = false;
				}
				structural[intTrio] = new MathUtils.Tri(true, flag2);
				if (flag3)
				{
					TrySpreadStability(intTrio, flag2);
				}
			}
		}
	}

	public struct Voxel
	{
		public readonly Vector3 MoveVector;

		public readonly int GravityIndex;

		public readonly bool IsAffected;

		public readonly bool IsUnaffected;

		public readonly bool IsAttached;

		public readonly bool IsActive;

		public Transform Transform;

		private Color? color;

		public Voxel(Vector3 position, Vector3 force, bool affected, bool detached, bool destroyed, int gravityIndex, Color? voxelColor)
		{
			GravityIndex = gravityIndex;
			color = voxelColor;
			IsAffected = affected;
			IsUnaffected = !affected;
			IsAttached = !detached;
			IsActive = detached;
			Transform = ((!destroyed) ? voxelBuffer.Get() : null);
			if (!destroyed)
			{
				if (IsAffected)
				{
					position.x += Randomizer.GetRandomSigned() * 0.005f;
					position.z += Randomizer.GetRandomSigned() * 0.005f;
				}
				Transform.position = position;
				bool hasValue = color.HasValue;
			}
			MoveVector = force;
		}

		public void Color(Color newColor)
		{
		}

		public void Destroy()
		{
			voxelBuffer.Give(Transform);
			Transform = null;
			color = null;
		}

		public bool Update(float voxelGravity, bool voxelGravityActive)
		{
			if (voxelGravityActive)
			{
				Transform.position = Transform.position + MoveVector + Vector3.down * voxelGravity;
			}
			else
			{
				Transform.position += MoveVector * 0.1f;
			}
			bool flag = Transform.position.y > -0.025f;
			if (!flag)
			{
				Destroy();
			}
			return flag;
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugBlockCreation = true;

	private const bool useVoxelDebugColors = false;

	private const bool useDetonationMarkers = false;

	private const string buildingTag = "Building";

	private const string bufferedGroupName = "_Buffered";

	private const int voxelsToBuffer = 2000;

	private const int markersToBuffer = 100;

	private const float voxelSize = 0.05f;

	private const float voxelSizeHalf = 0.025f;

	private const float voxelSizeQuarter = 0.0125f;

	private const float voxelRndMovement = 0.005f;

	private const float defaultDestructiveForce = 0.5f;

	private const float defaultHitMargin = 0.1f;

	private const float gravity = 0.1f;

	private const float fallGravity = 0.01f;

	private const float fallLevelDelay = 0.04f;

	private const float fallLevelDelayedForceMultiplier = 0.1f;

	private const float impactForceMultiplier = 0.2f;

	private const float toppleForceMultiplier = 0.0075f;

	private const float toppleRandomForce = 0.5f;

	private const float toppleLift = 0.1f;

	private const int maxInvalidMoves = 10;

	private static ArrayUtils.List<Building> buildings;

	private static GeoBuffer voxelBuffer;

	private static GeoBuffer markerBuffer;

	private static bool isInitialized;

	private static Transform bufferedGroup;

	private static float gravityForce;

	public static bool VoxelsAreActive { get; private set; }

	public static int TotalActive
	{
		get
		{
			return voxelBuffer.Active;
		}
	}

	public static int TotalCreated
	{
		get
		{
			return voxelBuffer.Created;
		}
	}

	public static void Initialize(Transform masterVoxelGeo, Transform masterMarkerGeo, Material markerMaterial1, Material markerMaterial2)
	{
		if (isInitialized)
		{
			return;
		}
		Transform[] array = TransformUtils.FindAllTagged("Building");
		if (array == null || array.Length == 0)
		{
			Debug.LogError(string.Format("BKMN: ERROR: Unable to find any objects with the tag '{0}' to convert into voxelable buildings.  Please tag all objects you wish to be made out of voxels with the '{0}' tag", "Building"));
		}
		else
		{
			buildings = new ArrayUtils.List<Building>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				buildings.Add(new Building(array[i]));
			}
		}
		bufferedGroup = new GameObject("_Buffered").transform;
		Object.DontDestroyOnLoad(bufferedGroup.gameObject);
		voxelBuffer = new GeoBuffer(masterVoxelGeo, 2000);
		VoxelsAreActive = false;
		isInitialized = true;
	}

	public static void UpdateBlocks()
	{
		if (!VoxelsAreActive)
		{
			return;
		}
		bool voxelsAreActive = false;
		gravityForce = 0.1f * Time.smoothDeltaTime;
		for (int i = 0; i < buildings.Length; i++)
		{
			if (buildings[i].IsActive && buildings[i].Update())
			{
				voxelsAreActive = true;
			}
		}
		VoxelsAreActive = voxelsAreActive;
	}

	public static void Impact(Vector3 impactPoint, float impactSize)
	{
		Impact(impactPoint, impactSize, 0.5f);
	}

	public static void Impact(Vector3 impactPoint, float impactSize, float destructivity)
	{
		bool flag = false;
		for (int i = 0; i < buildings.Length; i++)
		{
			if (buildings[i].Contains(impactPoint, impactSize))
			{
				buildings[i].Impact(impactPoint, impactSize, destructivity);
				flag = true;
			}
		}
		if (flag)
		{
			VoxelsAreActive = true;
		}
	}

	public static void RandomImpact(int buildingNumber)
	{
		buildings[buildingNumber].RandomImpact();
		VoxelsAreActive = true;
	}

	public static void MouseImpact()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] array = Physics.RaycastAll(ray);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].transform.CompareTag("Building"))
			{
				continue;
			}
			int? num = null;
			for (int j = 0; j < buildings.Length; j++)
			{
				if (buildings[j].HasTransform(array[i].transform))
				{
					num = j;
					break;
				}
			}
			if (num.HasValue)
			{
				Vector3 point = array[i].point;
				if (buildings[num.Value].CanDetonate(ref point, ray.direction))
				{
					Impact(point, 0.25f);
					break;
				}
			}
			else
			{
				Debug.LogError(string.Format("BKMN: ERROR: Unable to find building matching transform tagged as {0}, named {1}", "Building", array[i].transform.name));
			}
		}
	}
}
