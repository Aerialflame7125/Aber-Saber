namespace System.Web.Profile;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event of a <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
/// <param name="sender">The <see cref="T:System.Web.Profile.ProfileModule" /> that raised the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event.</param>
/// <param name="e">A <see cref="T:System.Web.Profile.ProfileAutoSaveEventArgs" /> that contains the event data.</param>
public delegate void ProfileAutoSaveEventHandler(object sender, ProfileAutoSaveEventArgs e);
