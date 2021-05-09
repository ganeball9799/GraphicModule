using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GraphicModule.Models.Enums;


namespace GraphicModule.Models
{
    public class Geometry
    {
        public LinesStructure Structure;



        private List<Parameter> _parameters = new List<Parameter>
        {
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.StripsWidth, 30),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.Slot,20),
            new Parameter(ParameterName.StripsNumber,1),
            new Parameter(ParameterName.StripsNumber,2),
            new Parameter(ParameterName.StripsNumber,3),
            new Parameter(ParameterName.StripsNumber,4),
            new Parameter(ParameterName.StripsNumber,5),
            new Parameter(ParameterName.StripsNumber,6),
            new Parameter(ParameterName.StripsThickness,10),
            new Parameter(ParameterName.SubstrateHeight,20)
        };


    }
}
