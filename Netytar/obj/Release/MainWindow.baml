   M S B A M L   `   `   `  ���� ^  ZTobii.Interaction.Net, Version=0.7.3.333, Culture=neutral, PublicKeyToken=70326046dcdce6cb\Bclr-namespace:Tobii.Interaction.Wpf;assembly=Tobii.Interaction.NetTobii.Interaction.Wpf   Netytar   Netytar.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     TtobiiBclr-namespace:Tobii.Interaction.Wpf;assembly=Tobii.Interaction.Net       � Title$  
MainWindow�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�$
��Auto��$
��Auto��.��� ����  �� ImageSource$&  /Images/Backgrounds/DarkWood.png�� j� TileMode$
 Tile=� � ColumnDefinitions �� $
��219* ��� $��1147* �.+��� $��Stretch=�$��Stretch=�$��
20,60,10,0q�$
��Auto��$
��Auto��$��4q�
�������$��0������ $' !/Images/Backgrounds/LightWood.jpg��$
 Tile=�._��  �� $��1* � � RowDefinitions �� $	��58* ��� $	��58* ��� $	��80* ��� $	��51* ��� $	��66* ��� $	��59* ��� $	��58* ��� $	��59* �.+�� $��0�� �� $��1* ��� $��1* �.+��� $��1��$��2q�
�������$
��Auto��$
��Auto��$��5,5,5,5q�._��� -   	 ��Name$	 btnStart��
�������$��Start��$
��Auto��$
��Auto��$��Magneto)�$��22(�
 �� IsCancel
 . ���� $$ Images/Backgrounds/MidWood.png��$
 Tile=� j� Stretch$
 None=��� $��0��$��2q�
�������$
��Auto��$
��Auto��$��5,5,5,5q�._��� -   $��btnExit��
�������$
��Exit��$
��Auto��$
��Auto��$��Magneto)�$��22(�
 . ���� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=�� $��1�� �� $��1* ��� $��1* �.+��� -   $��VelocityBarBorder��$��1��$��5,5,5,5q�$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�
�������$��2q� �� CornerRadius$ 0��
���   �._��� -   $��VelocityBar��$
��Auto��$��10��$��0q�$ 0��$��Stretch=�$��Bottom=�
�������� -   $��MaxBar��$��1��$��5,5,5,5q�$
��Auto��$
��Auto��$��Stretch=�$��Bottom=�
�������$��1q�$ 0��
������ �� -    ��Name$ lstScaleChanger��$��0��$��5,5,5,5q�$��2q�
�������$
��Auto��$
��Auto��
���   �
�������� $��2��$��10,10,10,10q� �� $��40 ��� $
��Auto � �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� -   $��btnCtrlKeyboard��$��0��$��0��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -   $��btnCtrlBreath��$��0��$��1��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -	   $��btnCtrlEyePos��$��0��$��2��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -
   $��btnCtrlEyeVel��$��0��$��3��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -   $��indCtrlKeyboard��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��0��$��2q�
�������
���   ��� -   $��indCtrlBreath��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��1��$��2q�
�������
���   ��� -   $��indCtrlEyePos��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��2��$��2q�
�������
���   ��� -   $��indCtrlEyeVel��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��3��$��2q�
�������
���   ��� $��22,0,0,0q�$��1��$��0��
�������$��Keyboard��$��Magneto)�$��13(��� $��22,0,0,0q�$��1��$��1��
�������$��Breath��$��Magneto)�$��13(��� $��22,0,0,0q�$��1��$��2��
�������$��EyePos��$��Magneto)�$��13(��� $��22,0,0,0q�$��1��$��3��
�������$��EyeVel��$��Magneto)�$��13(�� $��3��$��5,5,5,5q� �� $��1* ��� $��1* ��� $��1* �.+��� $y�Visible=�
���   �$��0��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $��btnSensorPortMinus��
�������$��t��$
��Auto��$
��Auto��$��0,0,0,0q�$��Wingdings 3)�$��30(�
 . ���� $% Images/Backgrounds/DarkWood.png��$
 Tile=�$
 None=��� $y�Visible=�
���   �$��1��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $��txtSensorPort��$��0,14,0,0q�
���   �
�������$��Digital-7 Italic)�$��25(� �� TextAlignment$ Center=�$�� ���� $y�Visible=�
���   �$��2��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $��btnSensorPortPlus��
�������$��u��$
��Auto��$
��Auto��$��0,0,0,0q�$��Wingdings 3)�$��30(�
 . ���� $% Images/Backgrounds/DarkWood.png��$
 Tile=�$
 None=�� $��4��$��5,1,5,1q� �� $��1* ��� $��1* ��� $��1* �.+��� 
���   �$��0��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $	 btnMIDIchMinus��
�������$��t��$
��Auto��$
��Auto��$��0,1,1,0q�$��Wingdings 3)�$��30(�
 . ���� $% Images/Backgrounds/DarkWood.png��$
 Tile=�$
 None=��� 
���   �$��1��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $��	lblMIDIch��$��0,10,0,0q�
���   �
�������$��Digital-7 Italic)�$��28(�$ Center=�$	��MP1���� 
���   �$��2��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $	 btnMIDIchPlus��
�������$��u��$
��Auto��$
��Auto��$��Wingdings 3)�$��30(�
 . ���� $% Images/Backgrounds/DarkWood.png��$
 Tile=�$
 None=�� $��5��$��5,1,5,1q� �� $��1* ��� $��1* ��� $��1* �.+��� 
���   �$��0��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -    ��Name$ txtPitch��$��0,10,0,0q�
���   �
�������$��Digital-7 Italic)�$��35(�$ Center=�$��60���� 
���   �$��2��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $ txtIsBlowing��$��0,10,0,0q�
���   �
�������$��Digital-7 Italic)�$��35(�$ Center=�$��_���� 
���   �$��1��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $ txtNoteName��$��0,10,0,0q�
���   �
�������$��Digital-7 Italic)�$��35(�$ Center=�$��C��� $��6��$��10,10,10,10q� �� $��40 ��� $
��Auto � �� $��1* ��� $��1* �.+��� -   $ ��btnModulationControlSwitch��$��0��$��0��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -   $��btnBreathControlSwitch��$��0��$��1��$��2q�
�������$
��Auto��$
��Auto��$��Stretch=�$��Stretch=����� $$ Images/Backgrounds/MidWood.png��$
 Tile=�$
 None=��� -   $��indModulationControl��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��0��$��2q�
�������
���   ��� -   $��indBreathSwitch��$��4,0,0,0q�$��20��$��20��$ 10��$
��Left=�$��Stretch=�$��1��$��1��$��2q�
�������
���   ��� $��22,0,0,0q�$��1��$��0��
�������$	��Mod��$��Magneto)�$��18(��� $��22,0,0,0q�$��1��$��1��
�������$��BSwitch��$��Magneto)�$��18(�� $��7��$��5,1,5,1q� �� $��1* ��� $��1* �.+��� $y�Visible=�$��1��$��2q�
�������$
��Auto��$
��Auto��._��� -   $��btnCalibrateHeadPose��
�������$�� ��$
��Auto��$
��Auto��$��Magneto)�$��22(�
 . ����  �� Color$ Black���� $y�Hidden=�
���   �$��1��$��2q�
�������$
��Auto��$��60��$��1,1,1,1q�._��� -   $	 
btnFlasher��
�������$��~��$
��Auto��$
��Auto��$��0,1,1,0q�$��Webdings)�$��30(�
 . ���� $% Images/Backgrounds/DarkWood.png��$
 Tile=�$
 None=��� $y�Visible=�$��0��$��1��
���   �$
��Auto��$
��Auto��._��� -   $��txtTest��$��0,0,0,0q�
���   �
�������$��Arial)�$��13(�$
 Left=�$�� ���� $��Stretch=�$��Stretch=�$��
10,60,20,0q�$
��Auto��$
��Auto��$��4q�
�������$��1������ $&  Images/Backgrounds/LightWood.jpg��$
 Tile=�._��� -    ��Name$ tabSolo��$��	-6,-6,6,6q�.@��� -     ��Name$ 
tabNetytar��$��Netytar��$y�	Collapsed=�.��� ���� $ Black�� �� $	��12* �.+��� -!    ��Name$ scrlNetytar��$��Stretch=�$��Stretch=�$��0,0,0,0q�$
��Auto��$
��Auto��$��Visible=�$��Visible=�$��0��.���� -"    ��Name$ canvasNetytar��$��Stretch=�$��Stretch=�$��0,0,0,0q��� -#    ��Name$ 	lblRhythm��$��Settings��$
��Left=�$��5,5,0,0q�$	��Top=�$��Magneto)�$��28(�
�������$��44��$	��162���� -$   $ lblSolo��$
��Solo��$��Right=�$��0,5,5,0q�$	��Top=�$��Magneto)�$��28(�
�������$��1��$��44��$��80��