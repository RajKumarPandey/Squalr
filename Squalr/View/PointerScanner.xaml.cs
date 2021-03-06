﻿namespace Squalr.View
{
    using Squalr.Source.Scanners.Pointers;
    using SqualrCore.Source.Controls;
    using SqualrCore.Source.Utils;
    using SqualrCore.Source.Utils.Extensions;
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for PointerScanner.xaml.
    /// </summary>
    internal partial class PointerScanner : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualScanner"/> class.
        /// </summary>
        public PointerScanner()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            this.ValueHexDecBox = new HexDecTextBox(typeof(UInt64));
            this.ValueHexDecBox.IsHex = true;
            this.ValueHexDecBox.TextChanged += this.ValueUpdated;
            this.valueHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ValueHexDecBox));

            this.DepthHexDecBox = new HexDecTextBox(typeof(UInt32));
            this.DepthHexDecBox.TextChanged += this.DepthUpdated;
            this.DepthHexDecBox.SetValue(PointerScannerViewModel.DefaultPointerScanDepth);
            this.depthHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(this.DepthHexDecBox));

            this.PointerRadiusHexDecBox = new HexDecTextBox(typeof(UInt32));
            this.PointerRadiusHexDecBox.TextChanged += this.PointerRadiusUpdated;
            this.PointerRadiusHexDecBox.SetValue(PointerScannerViewModel.DefaultPointerScanRadius);
            this.pointerRadiusHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(this.PointerRadiusHexDecBox));
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public PointerScannerViewModel PointerScannerViewModel
        {
            get
            {
                return this.DataContext as PointerScannerViewModel;
            }
        }

        /// <summary>
        /// Gets or sets the value hex dec box used to display the current value being edited.
        /// </summary>
        private HexDecTextBox ValueHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the value hex dec box used to display the current depth being edited.
        /// </summary>
        private HexDecTextBox DepthHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the value hex dec box used to display the current pointer radius being edited.
        /// </summary>
        private HexDecTextBox PointerRadiusHexDecBox { get; set; }

        /// <summary>
        /// Invoked when the current value is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void ValueUpdated(Object sender, EventArgs e)
        {
            Object value = this.ValueHexDecBox.GetValue();
            this.PointerScannerViewModel.SetAddressCommand.Execute(value == null ? 0 : Conversions.ParsePrimitiveStringAsPrimitive(typeof(UInt64), value.ToString()));
        }

        /// <summary>
        /// Invoked when the current value is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void DepthUpdated(Object sender, EventArgs e)
        {
            Object value = this.DepthHexDecBox.GetValue();
            UInt32 realValue = value == null ? 0 : (UInt32)Conversions.ParsePrimitiveStringAsPrimitive(typeof(UInt32), value.ToString());

            if (this.DepthHexDecBox.IsValid())
            {
                this.DepthHexDecBox.SetValue(realValue.Clamp<UInt32>(0, PointerScannerViewModel.MaximumPointerScanDepth));
            }

            this.PointerScannerViewModel.SetDepthCommand.Execute(realValue);
        }

        /// <summary>
        /// Invoked when the current value is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void PointerRadiusUpdated(Object sender, EventArgs e)
        {
            Object value = this.PointerRadiusHexDecBox.GetValue();
            this.PointerScannerViewModel.SetPointerRadiusCommand.Execute(value == null ? 0 : Conversions.ParsePrimitiveStringAsPrimitive(typeof(UInt32), value.ToString()));
        }
    }
    //// End class
}
//// End namespace