using System.Collections;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing;

/// <summary>Animates an image that has time-based frames.</summary>
public sealed class ImageAnimator
{
	private static Hashtable ht = Hashtable.Synchronized(new Hashtable());

	private ImageAnimator()
	{
	}

	/// <summary>Displays a multiple-frame image as an animation.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to animate.</param>
	/// <param name="onFrameChangedHandler">An <see langword="EventHandler" /> object that specifies the method that is called when the animation frame changes.</param>
	public static void Animate(Image image, EventHandler onFrameChangedHandler)
	{
		if (CanAnimate(image) && !ht.ContainsKey(image))
		{
			byte[] value = image.GetPropertyItem(20736).Value;
			int[] array = new int[value.Length >> 2];
			int num = 0;
			int num2 = 0;
			while (num < value.Length)
			{
				int num3 = BitConverter.ToInt32(value, num) * 10;
				array[num2] = ((num3 < 100) ? 100 : num3);
				num += 4;
				num2++;
			}
			AnimateEventArgs animateEventArgs = new AnimateEventArgs(image);
			Thread thread = new Thread(new WorkerThread(onFrameChangedHandler, animateEventArgs, array).LoopHandler);
			thread.IsBackground = true;
			animateEventArgs.RunThread = thread;
			ht.Add(image, animateEventArgs);
			thread.Start();
		}
	}

	/// <summary>Returns a Boolean value indicating whether the specified image contains time-based frames.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to test.</param>
	/// <returns>This method returns <see langword="true" /> if the specified image contains time-based frames; otherwise, <see langword="false" />.</returns>
	public static bool CanAnimate(Image image)
	{
		if (image == null)
		{
			return false;
		}
		int num = image.FrameDimensionsList.Length;
		if (num < 1)
		{
			return false;
		}
		for (int i = 0; i < num; i++)
		{
			if (image.FrameDimensionsList[i].Equals(FrameDimension.Time.Guid))
			{
				return image.GetFrameCount(FrameDimension.Time) > 1;
			}
		}
		return false;
	}

	/// <summary>Terminates a running animation.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to stop animating.</param>
	/// <param name="onFrameChangedHandler">An <see langword="EventHandler" /> object that specifies the method that is called when the animation frame changes.</param>
	public static void StopAnimate(Image image, EventHandler onFrameChangedHandler)
	{
		if (image != null && ht.ContainsKey(image))
		{
			((AnimateEventArgs)ht[image]).RunThread.Abort();
			ht.Remove(image);
		}
	}

	/// <summary>Advances the frame in all images currently being animated. The new frame is drawn the next time the image is rendered.</summary>
	public static void UpdateFrames()
	{
		foreach (Image key in ht.Keys)
		{
			UpdateImageFrame(key);
		}
	}

	/// <summary>Advances the frame in the specified image. The new frame is drawn the next time the image is rendered. This method applies only to images with time-based frames.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> object for which to update frames.</param>
	public static void UpdateFrames(Image image)
	{
		if (image != null && ht.ContainsKey(image))
		{
			UpdateImageFrame(image);
		}
	}

	private static void UpdateImageFrame(Image image)
	{
		AnimateEventArgs animateEventArgs = (AnimateEventArgs)ht[image];
		image.SelectActiveFrame(FrameDimension.Time, animateEventArgs.GetNextFrame());
	}
}
