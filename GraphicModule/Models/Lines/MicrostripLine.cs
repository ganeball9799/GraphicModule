using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicModule.Models.Enums;

namespace GraphicModule.Models
{
    // А этот класс решил не наследовать от geometry?
    public class MicrostripLine: Geometry
    {

        public LinesStructure Type = LinesStructure.Microstrip;

        public List<Parameter> _parameters = new List<Parameter>
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
