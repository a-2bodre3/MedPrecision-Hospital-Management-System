import { Component, inject, signal } from '@angular/core';
import {
  LucideEye,
  LucideEyeOff,
  LucideLockKeyhole,
  LucideLogIn,
  LucideMail,
} from '@lucide/angular';
import { AuthStore } from '../../state/auth.store';
import { LoginDto } from '../../model/login-dto.model';
import { email, form, FormField, pattern, required } from '@angular/forms/signals';
import { HlmFieldImports } from '@spartan-ng/helm/field';
import { HlmInputGroupImports } from '@spartan-ng/helm/input-group';
import { HlmButtonImports } from '@spartan-ng/helm/button';
import { toast } from '@spartan-ng/brain/sonner';
import { HlmSpinnerImports } from '@spartan-ng/helm/spinner';

@Component({
  selector: 'app-login',
  imports: [
    LucideMail,
    LucideLockKeyhole,
    LucideLogIn,
    FormField,
    HlmFieldImports,
    HlmInputGroupImports,
    HlmButtonImports,
    HlmSpinnerImports,
    LucideEyeOff,
    LucideEye,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  //===================================================
  //===================inject==========================
  //===================================================
  protected authStore = inject(AuthStore);

  //===================================================
  //===================variable========================
  //===================================================
  protected showPassword = signal<boolean>(false);
  //===================================================
  //===================form============================
  //===================================================
  protected loginModel = signal<LoginDto>({
    email: '',
    password: '',
  });

  protected loginForm = form(this.loginModel, (schemaPath) => {
    //--------------email--------------------
    required(schemaPath.email, { message: 'البريد الالكتروني مطلوب' });
    email(schemaPath.email, { message: 'البريد الالكتروني غير صالح' });
    //-------------password--------------------
    required(schemaPath.password, { message: 'كلمة المرور مطلوبة' });
    pattern(
      schemaPath.password,
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
      {
        message:
          'يجب أن تتكون كلمة المرور من 8 أحرف على الأقل وأن تحتوي على حرف كبير واحد على الأقل، وحرف صغير واحد على الأقل، ورقم واحد على الأقل، ورمز خاص واحد على الأقل.',
      },
    );
  });

  //===================================================
  //==================method===========================
  //===================================================
  async submit(event: Event) {
    event.preventDefault();
    const loginData: LoginDto = {
      email: this.loginModel().email,
      password: this.loginModel().password,
    };
    if (this.loginForm().invalid()) {
      toast.error('اكتب فورم صالح و اكمل البيانات');
      return;
    }
    try {
      await this.authStore.loginUser(loginData);
      this.loginModel.set({
        email: '',
        password: '',
      });
      this.loginForm().reset();
    } catch (error) {
      console.error(error);
    }
  }
}
