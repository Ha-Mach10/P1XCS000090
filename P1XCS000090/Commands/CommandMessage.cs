using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace P1XCS000090.Commands
{
	public class CommandMessage
	{
		// *******************************************************************************
		// Properties
		// *******************************************************************************

		public CommandState CommandState { get; }
		public EditState EditState { get; }



		// *******************************************************************************
		// Constructor
		// *******************************************************************************
		public CommandMessage()
		{

		}
	}
}
