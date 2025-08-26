using UnityEngine;

public class NoteDebrisSpawner : MonoBehaviour
{
	[SerializeField]
	private NoteDebris _debrisPrefab;

	private void Awake()
	{
		_debrisPrefab.CreatePool(30);
	}

	public void SpawnDebris(NoteCutInfo noteCutInfo, NoteController noteController)
	{
		Vector3 cutPoint = noteCutInfo.cutPoint;
		Vector3 cutNormal = noteCutInfo.cutNormal;
		float num = Vector3.Dot(cutNormal, Vector3.up);
		float num2 = (num + 1f) * 0.5f * 7f + 1f;
		float num3 = (0f - num + 1f) * 0.5f * 7f + 1f;
		float num4 = 4.5f;
		NoteDebris noteDebris = _debrisPrefab.Spawn(Vector3.zero, Quaternion.identity);
		NoteDebris noteDebris2 = _debrisPrefab.Spawn(Vector3.zero, Quaternion.identity);
		NoteType noteType = noteController.noteData.noteType;
		Transform noteTransform = noteController.noteTransform;
		noteDebris.Init(noteType, noteTransform, cutPoint, -cutNormal, -(cutNormal + Random.onUnitSphere * 0.2f) * num3, Random.insideUnitSphere * num4);
		noteDebris2.Init(noteType, noteTransform, cutPoint, cutNormal, (cutNormal + Random.onUnitSphere * 0.2f) * num2, Random.insideUnitSphere * num4);
	}
}
