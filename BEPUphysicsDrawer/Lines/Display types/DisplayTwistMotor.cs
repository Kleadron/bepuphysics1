using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUphysics.Constraints.TwoEntity.Motors;
using Microsoft.Xna.Framework;


namespace BEPUphysicsDrawer.Lines
{
    /// <summary>
    /// Graphical representation of a twist joint
    /// </summary>
    public class DisplayTwistMotor : SolverDisplayObject<TwistMotor>
    {
        private readonly Line axisA;
        private readonly Line axisB;


        public DisplayTwistMotor(TwistMotor constraint, LineDrawer drawer)
            : base(drawer, constraint)
        {
            axisA = new Line(Color.DarkRed, Color.DarkRed, drawer);
            axisB = new Line(Color.DarkRed, Color.DarkRed, drawer);
            myLines.Add(axisA);
            myLines.Add(axisB);
        }


        /// <summary>
        /// Moves the constraint lines to the proper location relative to the entities involved.
        /// </summary>
        public override void Update()
        {
            //Move lines around
            axisA.PositionA = LineObject.ConnectionA.Position;
            axisA.PositionB = LineObject.ConnectionA.Position + LineObject.BasisA.PrimaryAxis;

            axisB.PositionA = LineObject.ConnectionB.Position;
            axisB.PositionB = LineObject.ConnectionB.Position + LineObject.BasisB.PrimaryAxis;
        }
    }
}