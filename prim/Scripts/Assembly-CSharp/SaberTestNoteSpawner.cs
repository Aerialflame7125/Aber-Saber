using System.Collections;
using UnityEngine;

public class SaberTestNoteSpawner : MonoBehaviour
{
	[SerializeField]
	private NoteController _notePrefab;

	private void Awake()
	{
	}

	private void Start()
	{
		_notePrefab.CreatePool(20);
		StartCoroutine(SpawnNoteAfterDelay(0f, NoteType.NoteA));
		StartCoroutine(SpawnNoteAfterDelay(0f, NoteType.NoteB));
	}

	private IEnumerator SpawnNoteAfterDelay(float delay, NoteType noteType)
	{
		yield return new WaitForSeconds(delay);
	}

	private void HandleNoteWasCutEvent(NoteController noteController, NoteCutInfo cutResults)
	{
	}
}
