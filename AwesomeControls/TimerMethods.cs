using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls
{
	public static class TimerMethods
	{
		private static Dictionary<Timer, Action<object[]>> actionsForTimer = new Dictionary<Timer, Action<object[]>>();
		private static Dictionary<Timer, object[]> paramsForTimer = new Dictionary<Timer, object[]>();

		public static Timer SetTimeout(double delay, Action<object[]> action, params object[] parameters)
		{
			Timer tmr = new Timer();
			tmr.Tick += tmr_Tick;
			actionsForTimer.Add(tmr, action);
			paramsForTimer.Add(tmr, parameters);
			tmr.Interval = (int)delay;
			tmr.Start();
			return tmr;
		}

		private static void tmr_Tick(object sender, EventArgs e)
		{
			Timer tmr = (sender as Timer);
			if (!actionsForTimer.ContainsKey(tmr)) return;

			Action<object[]> action = actionsForTimer[tmr];
			object[] parameters = paramsForTimer[tmr];
			action(parameters);
			
			tmr.Stop();

			actionsForTimer.Remove(tmr);
		}

		public static bool ClearTimeout(Timer tmr)
		{
			if (!tmr.Enabled) return false;
			tmr.Stop();

			actionsForTimer.Remove(tmr);
			paramsForTimer.Remove(tmr);
			return true;
		}
	}
}
