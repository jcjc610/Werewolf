//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class GameGif
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public string StartGame { get; set; }
        public string StartChaosGame { get; set; }
        public string VillagerDieImages { get; set; }
        public string WolfWin { get; set; }
        public string WolvesWin { get; set; }
        public string VillagersWin { get; set; }
        public string TannerWin { get; set; }
        public string LoversWin { get; set; }
        public string SerialKillerWins { get; set; }
        public string CultWins { get; set; }
        public string NoWinner { get; set; }
    
        public virtual LanguageVariant LanguageVariant { get; set; }
    }
}
