   M S B A M L   `   `   `  ����K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  5   	   E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  5   	   (    "Monitor.TaskView.View.SignInWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Monitor.TaskView.View     5   	   <controls'clr-namespace:Monitor.TaskView.Resource     5   	   -   5        $	��300��5	   	   $	��300��6     � 
ResizeMode$  NoResize=�5
   	   $	��370��5   	   $	��370��6    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$7 1/Monitor.TaskView;component/Resource/TaskView.ico��5   	   
���   �.��5      �  � ColumnDefinitions 5   
   �� 5      $	��51* �6   �� 5      ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5       .+�5#   
   � -   5        $��50��5#      $	��Top=�6   ���	#FF40A6D1$��2��6e   	 J� Effect	 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%      
   
BlurRadius$
 10Q�6#      ShadowDepth$ 3Q�63      	Direction$	 270Q�6C      Opacity$	 0.2Q�6S    5'      �� 5(      $
��Auto �6#   �� 5)      �� 5*      $
��Auto �6#   �� 5+      $��60 �6#   .+�5.      0   *Monitor.TaskView.Resource.HeaderDecoration  ]� 50      $��2��6   $��0��6,   $
��Left=�6<   $��50��6W   ��.  .+�51      �� -   5         ��Name$ AVARTA��51      $��25��6(   $��25��64    K� ZIndex$ 1��6?   $
��Left=�6P   $��	14,0,0,10q�6k   $��/Resource/net-on.png��6~   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 53      $�� Sign Up��6   $��Center=�6+   $��Center=�6M   
�������$��SemiBold"�54      $��21(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��0,0,0,0q�6k   55      �� 56      $��2��6   $��2��6-   $��Center=�6=   $��Right=�6X   $��0,0,3,0q�57      .+�5>      �� -   5        $��40��5>      $��40��6$   $	����60      CloseMinMaxButtonStyle#���   6C   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�5@      $��20(�6O   $��Bottom=�5A      |�.  6W   5B      5C      � 5E   
   $��0��6   $��1��6   $��Stretch=�6,   $��Stretch=�6J   $��2��6f   $��	0,0,0,-22q�6z    5F      �� 5G      $��* �6#   �� 5H      $��212.714 �6#   �� 5I      $��61.286 �6#    5K      �� 5L      $
��Auto �6    �� 5M      $
��Auto �6    �� 5N      $
��Auto �6    �� 5O      $
��Auto �6    �� 5P      $��* �6    .+�5R      �� -   5         ��Name$ ServerIP��5R      $��1��6(   $��2��68   $��0��6L   $��
0,20,20,20q�6Y   $��35��6m   $��Center=�5S         RoundedGlowingIPTextBoxStyle#���  68    � ToolTip$ 	Server IP��5T      $��1��6*   $��14(�67   {�6E   �� *IsAvailable+�� 5U      -   5        $ UserName��5U      $��1��6(   $��2��68   $��1��6L   $��	0,0,20,20q�6Y   $��35��6l   $��Center=�5V        RoundedGlowingTextBoxStyle#���  68   $ 	User Name��5W      $��1��6*   $��14(�67   {�6E   �� *IsAvailable+�� 5X      -   5        $ Company��5X      $��1��6'   $��2��67   $��2��6K   $��	0,0,20,20q�6X   $��35��6k   $��Center=�5Y      #���  68   $ Company��5Z      $��1��6(   $��14(�65   {�6C   �� *IsAvailable+�� 5[      -   5        $ WorkDirectory��5[      $��Center=�6-     SelectFileTextBoxStyle#���  5\      ���	#FFFFFFFF���	#FFABADB3$��1q�5]   2   $��14(�6H   
���   �$ Work Directory��5^      $��1��6/   $��1��6�   $��3��6�   {�6s   �� *IsAvailable+6�   �� 5_      -	   5        $��Right=�5_      $	��...��6,   $	��1 0q�6:   $��14(�6H   
����������	#FFD6D6D6���	#FFD6D6D6 J� 	AllowDrop .   SelectFileButtonStyle#���  5a      $��2��6s   $��0,0,20,0q�6�   $��3��6�   �� 5b      $��1��6   $��4��6)    �� Orientation$ Vertical=�66   $��	0,22,10,0q�6M   $��2��6`   .+�5c      �� $ 
Horizontal=�6   $��0 0 0 0q�66   $ 
0.51,1.127&�6i   {�6G   �� *IsAvailable+.+�5d      �� -
   5         ��Name$ RegistryButton��5d      $��Registry��65     RoundedButtonStyle#���  6H   $��	150 0 0 0q�5e      $��Right=�6*   $
��25 5q�6F   $��SemiBold"�6U   
����������	#FF40A6D1$��14(�5f   0   $��3��6]   $ 1.779,0.903&�6j   $��35��6�   ��5e   ~   Z� 5f   �   5g      5h      �� 5i      $��	Server IP��6   $
��left=�6(   $��	13,23,0,0q�6C   $	��Top=�6V   $��Right=�6n   $��Center=�6�   $��35��6�   $��68��6�   �� 5j      $��Username��6   $
��left=�6'   $��19,3,0,0q�6B   $	��Top=�6T   $��Right=�6l   $��Center=�6�   $��35��6�   $��68��6�   $��1��6�   �� 5k      $��Company��6   $��Center=�6&   $��18,3,4,0q�6C   $	��Top=�6U   $��Right=�6m   $��Center=�6�   $��35��6�   $��68��6�   $��2��6�   �� 5l      $��Work Folder��6   $��right=�6*   $��0,3,4,0q�6F   $	��Top=�6W   $��Right=�6o   $��Center=�6�   $��35��6�   $��86��6�   $��3��6�   5m      5n      5o      