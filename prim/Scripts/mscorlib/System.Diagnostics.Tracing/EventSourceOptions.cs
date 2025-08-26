namespace System.Diagnostics.Tracing;

/// <summary>Specifies overrides of default event settings such as the log level, keywords and operation code when the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> method is called.</summary>
public struct EventSourceOptions
{
	internal EventKeywords keywords;

	internal EventTags tags;

	internal EventActivityOptions activityOptions;

	internal byte level;

	internal byte opcode;

	internal byte valuesSet;

	internal const byte keywordsSet = 1;

	internal const byte tagsSet = 2;

	internal const byte levelSet = 4;

	internal const byte opcodeSet = 8;

	internal const byte activityOptionsSet = 16;

	/// <summary>Gets or sets the event level applied to the event.</summary>
	/// <returns>The event level for the event. If not set, the default is Verbose (5).</returns>
	public EventLevel Level
	{
		get
		{
			return (EventLevel)level;
		}
		set
		{
			level = checked((byte)value);
			valuesSet |= 4;
		}
	}

	/// <summary>Gets or sets the operation code to use for the specified event.</summary>
	/// <returns>The operation code to use for the specified event. If not set, the default is <see langword="Info" /> (0).</returns>
	public EventOpcode Opcode
	{
		get
		{
			return (EventOpcode)opcode;
		}
		set
		{
			opcode = checked((byte)value);
			valuesSet |= 8;
		}
	}

	internal bool IsOpcodeSet => (valuesSet & 8) != 0;

	/// <summary>Gets or sets the keywords applied to the event. If this property is not set, the event's keywords will be <see langword="None" />.</summary>
	/// <returns>The keywords applied to the event, or <see langword="None" /> if no keywords are set.</returns>
	public EventKeywords Keywords
	{
		get
		{
			return keywords;
		}
		set
		{
			keywords = value;
			valuesSet |= 1;
		}
	}

	/// <summary>The event tags defined for this event source.</summary>
	/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventTags" />.</returns>
	public EventTags Tags
	{
		get
		{
			return tags;
		}
		set
		{
			tags = value;
			valuesSet |= 2;
		}
	}

	/// <summary>The activity options defined for this event source.</summary>
	/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.</returns>
	public EventActivityOptions ActivityOptions
	{
		get
		{
			return activityOptions;
		}
		set
		{
			activityOptions = value;
			valuesSet |= 16;
		}
	}
}
