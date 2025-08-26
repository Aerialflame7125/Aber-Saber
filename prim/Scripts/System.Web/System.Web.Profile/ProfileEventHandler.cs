namespace System.Web.Profile;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.Profile.ProfileModule.Personalize" /> event of a <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
/// <param name="sender">The <see cref="T:System.Web.Profile.ProfileModule" /> that raised the <see cref="E:System.Web.Profile.ProfileModule.Personalize" /> event.</param>
/// <param name="e">A <see cref="T:System.Web.Profile.ProfileEventArgs" /> that contains the event data.</param>
public delegate void ProfileEventHandler(object sender, ProfileEventArgs e);
