namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class represents the weekly schedule for an Active Directory Domain Services replication.</summary>
public class ActiveDirectorySchedule
{
	/// <summary>Gets or sets a three-dimensional array that indicates at what time during the week that the source is available for replication.</summary>
	/// <returns>A three-dimensional array of <see cref="T:System.Boolean" /> elements in which the element is <see langword="true" /> if the source is available for replication during that specific 15-minute interval. The element is <see langword="false" /> if the source is not available for replication.  
	///  The array is in the form RawSchedule[&lt;day-of-week&gt;, &lt;hour&gt;, &lt;15-minute interval&gt;]. All of these values are zero-based and the week begins at 00:00 on Sunday morning, Coordinated Universal Time.  
	///  The following are the valid values for the day-of-week.  
	///   Day-of-week value  
	///
	///   Indicated day of the week.  
	///
	///   0  
	///
	///   Sunday  
	///
	///   1  
	///
	///   Monday  
	///
	///   2  
	///
	///   Tuesday  
	///
	///   3  
	///
	///   Wednesday  
	///
	///   4  
	///
	///   Thursday  
	///
	///   5  
	///
	///   Friday  
	///
	///   6  
	///
	///   Saturday  
	///
	///
	///
	///  The hour is zero-based and specified in 24-hour format. For example, 2 P.M. would be specified as 14. Valid values are 0-23.  
	///  The 15-minute interval specifies the 15-minute block within the hour that the source is available for replication. The following table identifies the possible values for the 15-minute interval.  
	///   15-minute interval  
	///
	///   Description  
	///
	///   0  
	///
	///   The source is available for replication from 0 to 14 minutes after the hour.  
	///
	///   1  
	///
	///   The source is available for replication from 15 to 29 minutes after the hour.  
	///
	///   2  
	///
	///   The source is available for replication from 30 to 44 minutes after the hour.  
	///
	///   3  
	///
	///   The source is available for replication from 45 to 59 minutes after the hour.  
	///
	///
	///
	///  The following C# example shows how to use this property to determine if the source is available for replication at 15:50 Coordinated Universal Time on Tuesday.  
	/// Boolean isAvailable = scheduleObject.RawSchedule[2, 15, 3];  
	///  The following C# example shows how to use this property to calculate the 15-minute interval at runtime by dividing the minutes by 15.  
	/// Boolean isAvailable = scheduleObject.RawSchedule[2, 15, (Int32)50/15];</returns>
	public bool[,,] RawSchedule
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class.</summary>
	public ActiveDirectorySchedule()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object.</summary>
	/// <param name="schedule">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object that is copied to this object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="schedule" /> is <see langword="null" />.</exception>
	public ActiveDirectorySchedule(ActiveDirectorySchedule schedule)
		: this()
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a range of times for a single day to the schedule.</summary>
	/// <param name="day">One of the <see cref="T:System.DayOfWeek" /> members that specifies the day of the week that the source will be available for replication.</param>
	/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
	/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
	/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
	/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
	/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="days" /> is <see langword="null" />.</exception>
	public void SetSchedule(DayOfWeek day, HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a range of times for multiple days to the schedule.</summary>
	/// <param name="days">One of the <see cref="T:System.DayOfWeek" /> members that specifies the day of the week that the source will be available for replication.</param>
	/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
	/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
	/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
	/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
	/// <exception cref="T:System.ArgumentException">The start time is after the end time.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">One or more parameters is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="days" /> is <see langword="null" />.</exception>
	public void SetSchedule(DayOfWeek[] days, HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a range of times for every day of the week to the schedule.</summary>
	/// <param name="fromHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the first hour that the source will be available for replication.</param>
	/// <param name="fromMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the first 15-minute interval that the source will be available for replication.</param>
	/// <param name="toHour">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.HourOfDay" /> members that specifies the final hour that the source will be available for replication.</param>
	/// <param name="toMinute">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.MinuteOfHour" /> members that specifies the final 15-minute interval that the source will be available for replication.</param>
	public void SetDailySchedule(HourOfDay fromHour, MinuteOfHour fromMinute, HourOfDay toHour, MinuteOfHour toMinute)
	{
		throw new NotImplementedException();
	}

	/// <summary>Resets the entire schedule to unavailable.</summary>
	public void ResetSchedule()
	{
		throw new NotImplementedException();
	}
}
