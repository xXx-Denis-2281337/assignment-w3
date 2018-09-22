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
        public void Reverse_Rotations(int size, int d, List<Color> rotations)
        {
            if (size >= 2 && size <= 5 && d >= 1 && d <= size / 2)
            {
                var rubik = AdvancedRubik.Init(size);

                foreach (var r in rotations)
                    rubik.RotateClockwise(r, d);

                var reverseRotations = new List<Color>(rotations);
                reverseRotations.Reverse();

                foreach (var rr in reverseRotations)
                    rubik.RotateCounterClockwise(rr, d);

                rubik.Should().Match<AdvancedRubik>(r => r.Solved());
            }
        }

        [Scenario]
        public void Init(int size)
        {
            if (size >= 2 && size <= 5)
            {
                var rubik = AdvancedRubik.Init(size);
                rubik.Should().Match<AdvancedRubik>(r => r.Solved());
            }
        }

        [Scenario]
        public void Scramble(int size)
        {
            if (size >= 2 && size <= 5)
            {
                var rubik = AdvancedRubik.Init(size).Scramble();
                rubik.Should().Match<AdvancedRubik>(r => !r.Solved());
            }
        }

        [Scenario]
        public void RotateClockwise(int size, Color c, int d)
        {
            if (size >= 2 && size <= 5 && d >= 1 && d <= size / 2)
            {
                var rubik = AdvancedRubik.Init(5).RotateClockwise(c, d);
                rubik.Should().Match<AdvancedRubik>(r => !r.Solved());
            }
        }

        [Property]
        public void Idempotent_Rotations(int size, Color c, int d)
        {
            if (size >= 2 && size <= 5 && d >= 1 && d <= size / 2)
            {
                var rubik = AdvancedRubik.Init(size);

                rubik.RotateClockwise(c, d).RotateClockwise(c, d).RotateClockwise(c, d).RotateClockwise(c, d);

                rubik.Should().Match<AdvancedRubik>(r => r.Solved());
            }
        }
    }
}