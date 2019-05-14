using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeeqDMIs.Music
{
    public class Scale
    {
        #region Fields
        private ScaleCodes scaleCode;
        private AbsNotes rootNote;
        private List<AbsNotes> notesInScale;
        #endregion

        #region Props
        public ScaleCodes ScaleCode { get => scaleCode; set => scaleCode = value; }
        public AbsNotes RootNote { get => rootNote; set => rootNote = value; }
        public List<AbsNotes> NotesInScale { get => notesInScale; set => notesInScale = value; }
        #endregion

        private static readonly List<AbsNotes> scaleNotes = new List<AbsNotes>()
        {
            AbsNotes.C, AbsNotes.sC, AbsNotes.D, AbsNotes.sD, AbsNotes.E, AbsNotes.F, AbsNotes.sF, AbsNotes.G, AbsNotes.sG, AbsNotes.A, AbsNotes.sA, AbsNotes.B,
            AbsNotes.C, AbsNotes.sC, AbsNotes.D, AbsNotes.sD, AbsNotes.E, AbsNotes.F, AbsNotes.sF, AbsNotes.G, AbsNotes.sG, AbsNotes.A, AbsNotes.sA, AbsNotes.B,
            AbsNotes.C, AbsNotes.sC, AbsNotes.D, AbsNotes.sD, AbsNotes.E, AbsNotes.F, AbsNotes.sF, AbsNotes.G, AbsNotes.sG, AbsNotes.A, AbsNotes.sA, AbsNotes.B
        };

        public Scale(AbsNotes rootNote, ScaleCodes scaleCode)
        {
            this.scaleCode = scaleCode;
            this.rootNote = rootNote;
            NotesInScale = GenerateNotesList(rootNote, scaleCode);
        }

        /// <summary>
        /// Generates a scale starting from the root note and the scale code.
        /// </summary>
        /// <param name="rootNote"></param>
        /// <param name="scaleCode"></param>
        /// <returns>A list of strings representing absolute note values.</returns>
        private List<AbsNotes> GenerateNotesList(AbsNotes rootNote, ScaleCodes scaleCode)
        {
            List<AbsNotes> resultScale = new List<AbsNotes>();
            int startingPos = 0;

            List<int> scaleRule = scaleCode.GetRule();

            // Determining starting pos
            for (int i = 0; i < scaleNotes.Count; i++)
            {
                if (rootNote == scaleNotes[i])
                {
                    startingPos = i;
                    break;
                }
            }

            // Adding elements to the returnable scale
            foreach (int pos in scaleRule)
            {
                resultScale.Add(scaleNotes[startingPos + pos]);
            }

            return resultScale;
        }

        /// <summary>
        /// Returns the name of the scale in string format (root note + scale code).
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return rootNote.ToString() + scaleCode.ToString();
        }

        /// <summary>
        /// Returns if the pitch corresponds to a note in the scale.
        /// </summary>
        /// <param name="midiPitch"></param>
        /// <returns></returns>
        public bool IsInScale(int midiPitch)
        {
            return IsInScale(PitchUtils.PitchToAbsNote(midiPitch));
        }

        /// <summary>
        /// Returns if the note is part of the scale.
        /// </summary>
        /// <param name="absNote"></param>
        /// <returns></returns>
        public bool IsInScale(AbsNotes absNote)
        {
            return scaleNotes.Contains(absNote);
        }

        /// <summary>
        /// Returns if the midi note is in scale.
        /// </summary>
        /// <param name="midiNote"></param>
        /// <returns></returns>
        public bool IsInScale(MidiNotes midiNote)
        {
            return IsInScale(midiNote.ToAbsNote());
        }

        public static Scale FromString(string name)
        {
            AbsNotes rootNote = AbsNotes.A;
            ScaleCodes code = ScaleCodes.chrom;
            bool rootNoteOK = false;
            bool codeOK = false;

            foreach(ScaleCodes scaleCode in Enum.GetValues(typeof(ScaleCodes)))
            {
                if (name.EndsWith(scaleCode.ToString()))
                {
                    code = scaleCode;
                    codeOK = true;
                }
            }
            foreach(AbsNotes note in Enum.GetValues(typeof(AbsNotes)))
            {
                if (name.StartsWith(note.ToString()))
                {
                    rootNote = note;
                    rootNoteOK = true;
                }
            }

            if(rootNoteOK && codeOK)
            {
                return new Scale(rootNote, code);
            }
            else
            {
                throw new Exception("The Scale class tried to parse an unexisting scale!");
            }
        }
    }
}
