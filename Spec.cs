using Xbehave;
using FluentAssertions;
using Xunit;
using FsCheck;
using FsCheck.Xunit;
using System.Collections.Generic;

namespace KmaOoad18.Assignments.Week3
{
    public class Spec
    {
        [Property]
        public bool Reverse_Rotations(List<Color> rotations)
        {
            var rubik = AdvancedRubik.Init(4);

            foreach (var r in rotations)
                rubik.RotateClockwise(r, 2);

            var reverseRotations = new List<Color>(rotations);
            reverseRotations.Reverse();

            foreach (var rr in reverseRotations)
                rubik.RotateCounterClockwise(rr, 2);

            return rubik.Solved();
        }

        [Scenario]
        public void Init()
        {            
            var rubik = AdvancedRubik.Init(5);
            rubik.Should().Match<AdvancedRubik>(r => r.Solved());
        }

        [Property]
        public bool Idempotent_Rotations(Color c)
        {
            var rubik = AdvancedRubik.Init(6);

            rubik.RotateClockwise(c,1).RotateClockwise(c,1).RotateClockwise(c,1).RotateClockwise(c,1);

            return rubik.Solved();
        }
    }
}