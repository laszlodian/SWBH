using System;
using System.Threading;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class CleanUpManager : System.IDisposable
    {
        public CleanUpManager()
        {
            GarbageCollecting();

            //Checking is any forms is available in background
            //if (ApplicationFormStatus.CheckIfFormIsOpen("FormName"))
            //{
            //    // It means it exists, so close the form
            //}

            //  foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(myMainform))
            //    {
            //        form.Activate();
            //        form.Show();
            //        this.Close();
            //        return;
            //    }
            //}

            //myMainform m = new myMainform();
            //m.Show();
        }

        public bool CheckIfFormIsOpen(string formname)
        {
            //FormCollection fc = Application.OpenForms;
            //foreach (Form frm in fc)
            //{
            //    if (frm.Name == formname)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            bool formOpen = Application.OpenForms.Count > 0 ? true : false;// Cast<Form>().Any(form => form.Name == formname);

            return formOpen;
        }

        private /*static*/void ExitInternal()
        {
            //bool exiting = true;
            //bool flag = false;
            //lock (internalSyncObject)
            //{
            //    if (exiting)
            //    {
            //        return false;
            //    }
            //    exiting = true;
            //    try
            //    {
            //        if (forms != null)
            //        {
            //            foreach (Form form in OpenFormsInternal.)
            //            {
            //                if (form.RaiseFormClosingOnAppExit())
            //                {
            //                    flag = true;
            //                    break;
            //                }
            //            }
            //        }
            //        if (!flag)
            //        {
            //            if (forms != null)
            //            {
            //                while (OpenFormsInternal.Count > 0)
            //                {
            //                    OpenFormsInternal[0].RaiseFormClosedOnAppExit();
            //                }
            //            }
            //            ThreadContext.ExitApplication();
            //        }
            //        return flag;
            //    }
            //    finally
            //    {
            //        exiting = false;
            //    }
            //}
            //return flag;
        }

        private static void GarbageCollecting()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}